﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_APi.model;
using Test_APi.Service;
using Test_APi.ServiceMpSeatService;

namespace Test_APi.Controllers
{
    [Route("api/mpseat")]
    //[ApiController]
    //[Authorize(Roles = "ElectionCommissioner")]
    public class MPSeatController : ControllerBase
    {
        IMPService IMPService;
        public MPSeatController(IMPService mPService)
        {
            IMPService = mPService;
        }
        [HttpPatch]
        public async Task<IActionResult> update(int mpSeatId, int seatsNumber)
        {
            try
            {
                var cc = await IMPService.update(mpSeatId, seatsNumber);
                if (cc != null)
                {
                    return Ok(cc);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpGet]

        public async Task<IActionResult> get()
        {
            try
            { 
            var cc = await IMPService.get();
            if (cc != null)
            {
                return Ok(cc);
            }
            else
            {
                return BadRequest();
            }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        // Action to decrease MP seats in a state
        public async Task<IActionResult> insertSeat(int seatsCount,int stateId)
        {
            try
            {
                var cc = await IMPService.insert(new MPSeat() { SeatNumber = seatsCount, StateId = stateId });
                if (cc != null)
                {
                    return Ok(cc);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

    }
}
