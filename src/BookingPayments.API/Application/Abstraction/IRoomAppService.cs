public interface IRoomAppService
{
    IList<Rooms> Select();
    void Create(Rooms room);
    bool Delete(int id);
    Rooms? GetById(int id);
    bool Edit(Rooms room);
}
