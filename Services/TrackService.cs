using lab1___blazor.Models;

namespace lab1___blazor.Services;

public class TrackService
{
    private readonly List<Track> _tracks;
    private int _nextId = 4;

    public TrackService()
    {
        _tracks = new List<Track>
        {
            new Track { Id = 1, Name = "Full Stack Web Development", Description = "Covers front-end and back-end web technologies including HTML, CSS, JavaScript, and ASP.NET Core." },
            new Track { Id = 2, Name = "Data Science & AI", Description = "Focuses on machine learning, data analysis, Python, and AI fundamentals." },
            new Track { Id = 3, Name = "Mobile Development", Description = "Cross-platform mobile app development using .NET MAUI and Blazor Hybrid." }
        };
    }

    public List<Track> GetAll() => _tracks;

    public Track? GetById(int id) => _tracks.FirstOrDefault(t => t.Id == id);

    public void Add(Track track)
    {
        track.Id = _nextId++;
        _tracks.Add(track);
    }

    public void Update(Track track)
    {
        var existing = GetById(track.Id);
        if (existing is null) return;

        existing.Name = track.Name;
        existing.Description = track.Description;
    }

    public void Delete(int id)
    {
        var track = GetById(id);
        if (track is not null)
            _tracks.Remove(track);
    }
}
