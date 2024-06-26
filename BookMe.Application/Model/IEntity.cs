namespace BookMe.Model;

public interface IEntity<TKey> where TKey : struct {
    TKey Id { get; }
}