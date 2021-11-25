namespace ReportsApp.WebApi.Controllers.Converters
{
    public interface IDtoConverter<TClientDto, TServerDto>
    {
        TServerDto Convert(TClientDto dto);
        
        TClientDto Convert(TServerDto dto);
    }
}