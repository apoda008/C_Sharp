#include <winsock2.h>
#include <iphlpapi.h>
#include <ws2tcpip.h>
#include <stdio.h>
#include "cJSON.h"

#pragma comment(lib, "iphlpapi.lib")
#pragma comment(lib, "Ws2_32.lib")

int lan_Get() {
    WSADATA wsaData;
    char hostname[256];
    struct addrinfo hints, * info, * p;
    char ipstr[INET6_ADDRSTRLEN];

    if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0) {
        printf("WSAStartup failed\n");
        return 1;
    }

    if (gethostname(hostname, sizeof(hostname)) == SOCKET_ERROR) {
        printf("gethostname failed: %d\n", WSAGetLastError());
        WSACleanup();
        return 1;
    }

    ZeroMemory(&hints, sizeof(hints));
    hints.ai_family = AF_INET;  // Change to AF_UNSPEC for IPv6 too
    hints.ai_socktype = SOCK_STREAM;

    if (getaddrinfo(hostname, NULL, &hints, &info) != 0) {
        printf("getaddrinfo failed\n");
        WSACleanup();
        return 1;
    }

    for (p = info; p != NULL; p = p->ai_next) {
        void* addr = &((struct sockaddr_in*)p->ai_addr)->sin_addr;
        inet_ntop(AF_INET, addr, ipstr, sizeof(ipstr));
        printf("LAN IP: %s\n", ipstr);
    }

    freeaddrinfo(info);
    WSACleanup();
    return 0;

}

int active_lan_get() {
    DWORD size = 0;
    ULONG flags = GAA_FLAG_INCLUDE_PREFIX;
    PIP_ADAPTER_ADDRESSES addresses = NULL, current = NULL;

    // First call to get buffer size
    GetAdaptersAddresses(AF_INET, flags, NULL, addresses, &size);
    addresses = (IP_ADAPTER_ADDRESSES*)malloc(size);

    if (GetAdaptersAddresses(AF_INET, flags, NULL, addresses, &size) != NO_ERROR) {
        printf("GetAdaptersAddresses failed.\n");
        free(addresses);
        return 1;
    }

    for (current = addresses; current != NULL; current = current->Next) {
        // Skip adapters that are not "up"
        if (current->OperStatus != IfOperStatusUp) continue;

        // Optional: skip loopback and virtual adapters
        if (current->IfType == IF_TYPE_SOFTWARE_LOOPBACK) continue;

        // Print adapter name
        printf("Adapter: %S\n", current->FriendlyName);

        // Iterate through IP addresses
        IP_ADAPTER_UNICAST_ADDRESS* unicast = current->FirstUnicastAddress;
        for (; unicast != NULL; unicast = unicast->Next) {
            struct sockaddr_in* sa = (struct sockaddr_in*)unicast->Address.lpSockaddr;
            char ipstr[INET_ADDRSTRLEN];
            inet_ntop(AF_INET, &(sa->sin_addr), ipstr, sizeof(ipstr));
            printf("  IPv4 Address: %s\n", ipstr);
        }
    }

    free(addresses);
    return 0;
}

int main() {
    lan_Get();
    printf("\nBREAK\n");
    active_lan_get();

    WSADATA wsaData;
    SOCKET sock;
    struct sockaddr_in server_addr;
    char buffer[4096];

    char parse_test_str[MAX_PATH] = "GET TITLE Firewalker";

    if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0) {
        printf("WSAStartUp Failed\n");
        return 1;
    }


    sock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    if (sock == INVALID_SOCKET) {
        printf("Socket failed: %d\n", WSAGetLastError());
        return 1;
    }

    server_addr.sin_family = AF_INET;
    server_addr.sin_port = htons(5001);
    inet_pton(AF_INET, "192.168.4.81", &server_addr.sin_addr);  // Replace with your server's LAN IP

    if (connect(sock, (SOCKADDR*)&server_addr, sizeof(server_addr)) == SOCKET_ERROR) {
        printf("Connect failed: %d\n", WSAGetLastError());
        closesocket(sock);
        WSACleanup();
        return 1;
    }

    //send(sock, "Hello from client", 18, 0);
    send(sock, parse_test_str, sizeof(parse_test_str), 0);
    
    
    int bytes_received = recv(sock, buffer, sizeof(buffer) - 1, 0);
    printf("bytes: %d\n", bytes_received);
    if (bytes_received > 0) {
        buffer[bytes_received] = '\0';
        //
        //printf("Received: %s\n", buffer);
        //
        cJSON* response = cJSON_Parse(buffer);
        char* json_str = cJSON_Print(response);
        printf("JSON: %s\n", json_str);
    }
    //send(sock, parse_test_str, sizeof(parse_test_str), 0);
    send(sock, "EXIT", 5, 0);
    closesocket(sock);
    WSACleanup();
    return 0;
}