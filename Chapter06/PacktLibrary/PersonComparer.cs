namespace Packt.Shared;

public class PersonComparer : IComparer<Person?> {
    public int Compare(Person? x, Person? y) 
    {
        int position;

        if ((x is not null) && (y is not null))
        {
            if ((x.Name is not null) && (y.Name is not null))
            {
                //if both name values are not null..
                //..then compare the name leng
                int result = x.Name.Length.CompareTo(y.Name.Length);

                //..and if they are equal
                if (result == 0)
                {
                    //.. then compare by the names 
                    return x.Name.CompareTo(y.Name);

                }
                else
                {
                    //... otherwise compare by lengths.
                    position = result;
                }
            }
            else if ((x.Name is not null) && (y.Name is null))
            {
                position = -1; //x person precedes y person 
            }
            else if ((x.Name is null) && (y.Name is not null))
            {
                position = 1; //x follows person y
            }
            else //x.name nad y.name are both null
            {
                position = 0; //x and y are at the same position 
            }

        }
        else if ((x is not null) && (y is null))
        {
            position = -1;
        }
        else if ((x is null) && (y is not null))
        {
            position = 1;
        }
        else 
        {
            position = 0; 
        }
        return position;
    }

}