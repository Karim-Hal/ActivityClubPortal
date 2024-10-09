using Microsoft.EntityFrameworkCore;
using SampleOneWebAPI.Data;
using SampleOneWebAPI.Models;
using SampleOneWebAPI.Repositories.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SampleOneWebAPI.Repositories
{
    public class EventRepository: IEventRepository
    {
        private readonly ActivityPortalDbContext _context;
        public EventRepository(ActivityPortalDbContext context)
        {
            _context = context;
        }
        public async Task<List<Event>> GetEvents(int? category, string? status, string? date, bool onlyOwned, int memberId)
        {
            IQueryable<Event> query;

            if (onlyOwned)
            {
                query = _context.EventMembers.Where(em => em.MemberId == memberId).Select(em => em.Event);
                 
            }
            else
            {
                query = _context.Events.AsQueryable(); // Start with a queryable
            }
           


            if (category != 0)
            {
                query = query.Where(e => e.CategoryId == category);
            }

            if (status != "Select Event Status")
            {
                query = query.Where(e => e.Status == status);
            }
           

         

            // Sorting date
            if (date == "asc")
            {
                query = query.OrderBy(e => e.DateFrom);
            }
            else if (date == "desc")
            {
                query = query.OrderByDescending(e => e.DateFrom);
            }

            // Execute query and return result
            return await query.ToListAsync();
        }
        public async Task<IEnumerable<Event>> GetAllEvents()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task EditEvent(Event eventModel)
        {
            var eventToUpdate = await _context.Events.FindAsync(eventModel.Id);

            if (eventToUpdate != null)
            {
               
                eventToUpdate.Name = eventModel.Name;
                eventToUpdate.Description = eventModel.Description;
                eventToUpdate.Destination = eventModel.Destination;
                eventToUpdate.DateFrom = eventModel.DateFrom;
                eventToUpdate.DateTo = eventModel.DateTo;
                eventToUpdate.Cost = eventModel.Cost;
                eventToUpdate.Status = eventModel.Status;
                eventToUpdate.CategoryId = eventModel.CategoryId;

                
                await _context.SaveChangesAsync();
            }
            else
            {
                // Handle case where event is not found
                throw new KeyNotFoundException($"Event with ID {eventModel.Id} not found.");
            }

        }

    }
}
