namespace ReportsApp.WebApi.Controllers.Students
{
    public interface ISpecification<TObject>
    {
        bool IsSatisfiedBy(TObject candidate);
        
        ISpecification<TObject> And(ISpecification<TObject> other);
    }
}