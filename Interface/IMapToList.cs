namespace GymProgress.Api.Interface
{
    public interface IMapToList<Source, Target>
    {
        List<Target> MapToList(List<Source> data);
    }
}
