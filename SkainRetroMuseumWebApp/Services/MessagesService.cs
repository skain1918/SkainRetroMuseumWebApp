using Microsoft.EntityFrameworkCore;
using SkainRetroMuseumWebApp.DTO;
using SkainRetroMuseumWebApp.Models;

namespace SkainRetroMuseumWebApp.Services;
public class MessagesService {
    private ApplicationDbContext _dbContext;

    public MessagesService(ApplicationDbContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<MessageDTO>> GetAllAsync() {
        var allMessages = await _dbContext.Messages
            .OrderByDescending(x => x.SentAt)
            .ToListAsync();
        var messageDtos = new List<MessageDTO>();
        foreach (var message in allMessages) {
            messageDtos.Add(new MessageDTO {
                Id = message.Id,
                Name = message.Name,
                Email = message.Email,
                Content = message.Content,
                SentAt = message.SentAt
            }
                );
        }
        return messageDtos;
    }

    public async Task CreateAsync(MessageDTO newMessage) {
        await _dbContext.Messages.AddAsync(new Message {
            Name = newMessage.Name,
            Email = newMessage.Email,
            Content = newMessage.Content,
            SentAt = DateTime.Now
        });
        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id) {
        var messageToDelete = await _dbContext.Messages.FirstOrDefaultAsync(m => m.Id == id);
        if (messageToDelete != null) {
            _dbContext.Remove(messageToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}
