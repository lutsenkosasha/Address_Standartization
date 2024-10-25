using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AddressStandardization
{
    public class AddressResult
    {
        public string Source { get; set; }
        public string Result { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string CountryIsoCode { get; set; }
        public string FederalDistrict { get; set; }
        public string RegionFiasId { get; set; }
        public string RegionKladrId { get; set; }
        public string RegionIsoCode { get; set; }
        public string RegionWithType { get; set; }
        public string RegionType { get; set; }
        public string RegionTypeFull { get; set; }
        public string Region { get; set; }
        public string AreaFiasId { get; set; }
        public string AreaKladrId { get; set; }
        public string AreaWithType { get; set; }
        public string AreaType { get; set; }
        public string AreaTypeFull { get; set; }
        public string Area { get; set; }
        public string CityFiasId { get; set; }
        public string CityKladrId { get; set; }
        public string CityWithType { get; set; }
        public string CityType { get; set; }
        public string CityTypeFull { get; set; }
        public string City { get; set; }
        public string CityArea { get; set; }
        public string CityDistrictFiasId { get; set; }
        public string CityDistrictKladrId { get; set; }
        public string CityDistrictWithType { get; set; }
        public string CityDistrictType { get; set; }
        public string CityDistrictTypeFull { get; set; }
        public string CityDistrict { get; set; }
        public string SettlementFiasId { get; set; }
        public string SettlementKladrId { get; set; }
        public string SettlementWithType { get; set; }
        public string SettlementType { get; set; }
        public string SettlementTypeFull { get; set; }
        public string Settlement { get; set; }
        public string StreetFiasId { get; set; }
        public string StreetKladrId { get; set; }
        public string StreetWithType { get; set; }
        public string StreetType { get; set; }
        public string StreetTypeFull { get; set; }
        public string Street { get; set; }
        public string SteadFiasId { get; set; }
        public string SteadKladrId { get; set; }
        public string SteadCadnum { get; set; }
        public string SteadType { get; set; }
        public string SteadTypeFull { get; set; }
        public string Stead { get; set; }
        public string HouseFiasId { get; set; }
        public string HouseKladrId { get; set; }
        public string HouseCadnum { get; set; }
        public string HouseType { get; set; }
        public string HouseTypeFull { get; set; }
        public string House { get; set; }
        public string BlockType { get; set; }
        public string BlockTypeFull { get; set; }
        public string Block { get; set; }
        public string Entrance { get; set; }
        public string Floor { get; set; }
        public string FlatFiasId { get; set; }
        public string FlatCadnum { get; set; }
        public string FlatType { get; set; }
        public string FlatTypeFull { get; set; }
        public string Flat { get; set; }
        public string FlatArea { get; set; }
        public string SquareMeterPrice { get; set; }
        public string FlatPrice { get; set; }
        public string PostalBox { get; set; }
        public string FiasId { get; set; }
        public string FiasCode { get; set; }
        public string FiasLevel { get; set; }
        public string FiasActualityState { get; set; }
        public string KladrId { get; set; }
        public string CapitalMarker { get; set; }
        public string Okato { get; set; }
        public string Oktmo { get; set; }
        public string TaxOffice { get; set; }
        public string TaxOfficeLegal { get; set; }
        public string Timezone { get; set; }
        public string GeoLat { get; set; }
        public string GeoLon { get; set; }
        public string BeltwayHit { get; set; }
        public string BeltwayDistance { get; set; }
        public int? QcGeo { get; set; }
        public int? QcComplete { get; set; }
        public int? QcHouse { get; set; }
        public int? Qc { get; set; }
        public string UnparsedParts { get; set; }
        public List<MetroStation> Metro { get; set; }
    }

    public class MetroStation
    {
        public double Distance { get; set; }
        public string Line { get; set; }
        public string Name { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly DadataService _dadataService;
        private readonly IMapper _mapper;

        public AddressController(DadataService dadataService, IMapper mapper)
        {
            _dadataService = dadataService;
            _mapper = mapper;
        }

        [HttpGet("standardize")]
        public async Task<IActionResult> StandardizeAddress([FromQuery] string rawAddress)
        {
            if (string.IsNullOrEmpty(rawAddress))
            {
                return BadRequest("Адрес не может быть пустым");
            }

            try
            {
                var result = await _dadataService.CleanAddress(rawAddress);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Логгирование ошибки
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Произошла ошибка при стандартизации адреса");
            }
        }
    }
}