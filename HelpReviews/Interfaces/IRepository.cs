namespace HelpReviews.Interfaces;

public interface IRepository<T, Tid>
{

    List<T> Get();

    T GetById(Tid id);

    T Create(T newData);

    public void Update(T updateData);

    public int Delete(Tid id);


}