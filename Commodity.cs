namespace econrpg
{
    public class Commodity
    {
        String Name;
        int id;

        static int commoditiesTotalNumber;

        public Commodity(String name)
        {
            this.Name = name;
            commoditiesTotalNumber++;
            this.id = commoditiesTotalNumber;
        }

        public String getName()
        {   
            return this.Name;
        }

        public int getId()
        {
            return this.id;
        }

        public override String ToString()
        {
            return $"Commodity {this.Name} / Id: {this.id}";
        }
    }
}