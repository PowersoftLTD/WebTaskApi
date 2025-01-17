using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplianceController : ControllerBase
    {
        private readonly ICompliance _repository;
        public ComplianceController(ICompliance repository)
        {
            _repository = repository;
        }

        [HttpPost("Get-Compliance")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ComplianceOutput_LIST>>> GetCompliance(ComplianceGetInput complianceGetInput)
        {
            try
            {
                if (complianceGetInput == null)
                {
                    var ErroResponse = new List<ComplianceOutput_LIST>
                            {
                                new ComplianceOutput_LIST
                                {
                                    STATUS= "Error",
                                    MESSAGE = "Please enter the details",
                                    DATA= null
                                }
                            };
                    return ErroResponse;
                }

                var RsponseStatus = await _repository.GetComplianceAsync(complianceGetInput);
                return RsponseStatus;
            }
            catch (Exception ex)
            {
                var ErroResponse = new List<ComplianceOutput_LIST>
                {
                    new ComplianceOutput_LIST
                    {
                        STATUS = "Error",
                        MESSAGE = ex.Message,
                        DATA= null
                    }
                };
                return ErroResponse;
            }
        }


        [HttpPost("Insert-Update-Compliance")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ComplianceOutput_LIST>>> InsertUpdateCompliance(ComplianceInsertUpdateInput complianceInsertUpdateInput)
        {
            try
            {
                if (complianceInsertUpdateInput == null)
                {
                    var response = new ComplianceOutput_LIST
                    {
                        STATUS = "Error",
                        MESSAGE = "Please Enter the details",
                        DATA = null
                    };
                    return Ok(response);
                }

                var RsponseStatus = await _repository.InsertUpdateComplianceAsync(complianceInsertUpdateInput);
                return RsponseStatus;
            }
            catch (Exception ex)
            {
                var response = new ComplianceOutput_LIST
                {
                    STATUS = "Error",
                    MESSAGE = ex.Message,
                    DATA = null
                };
                return Ok(response);
            }
        }
    }
}
