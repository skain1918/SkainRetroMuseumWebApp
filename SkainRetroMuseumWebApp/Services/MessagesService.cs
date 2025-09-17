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
            messageDtos.Add(mapToDto(message));
        }
        return messageDtos;
    }
    public async Task<MessageDTO?> GetByIdAsync(int id) {
        var message = await _dbContext.Messages.FirstOrDefaultAsync(m => m.Id == id);
        if (message == null) {
            return null;
        }
        return mapToDto(message);
    }

    public async Task CreateAsync(MessageDTO newMessage) {
        await _dbContext.Messages.AddAsync(new Message {
            Id = newMessage.Id,
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
    private static MessageDTO mapToDto(Message message) {
        return new MessageDTO {
            Id = message.Id,
            Name = message.Name,
            Email = message.Email,
            Content = message.Content,
            SentAt = message.SentAt
        };
    }
}
