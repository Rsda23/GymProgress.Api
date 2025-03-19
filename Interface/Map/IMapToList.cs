namespace GymProgress.Api.Interface.Map
{
    public interface IMapToList<Source, Target>
    {
        List<Target> MapToList(List<Source> data);
    }
}
