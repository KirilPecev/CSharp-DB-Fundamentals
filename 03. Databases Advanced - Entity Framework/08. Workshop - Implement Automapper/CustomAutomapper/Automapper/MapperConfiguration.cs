namespace Automapper
{
    public class MapperConfiguration
    {
        public Mapper Mapper { get; set; }

        public MapperConfiguration CreateMap()
        {
            this.Mapper = new Mapper();

            return this;
        }
    }
}
