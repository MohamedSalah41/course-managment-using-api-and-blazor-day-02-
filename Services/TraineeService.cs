using lab1___blazor.Models;

namespace lab1___blazor.Services;

public class TraineeService
{
    private readonly List<Trainee> _trainees;
    private int _nextId = 4;

    public TraineeService()
    {
        _trainees = new List<Trainee>
        {
            new Trainee
            {
                Id = 1,
                Name = "Ahmed Hassan",
                Gender = Gender.Male,
                Email = "ahmed.hassan@example.com",
                MobileNo = "+201001234567",
                Birthdate = new DateTime(2000, 5, 15),
                IsGraduated = false,
                TrackId = 1
            },
            new Trainee
            {
                Id = 2,
                Name = "Sara Mohamed",
                Gender = Gender.Female,
                Email = "sara.mohamed@example.com",
                MobileNo = "+201112345678",
                Birthdate = new DateTime(1999, 8, 22),
                IsGraduated = true,
                TrackId = 2
            },
            new Trainee
            {
                Id = 3,
                Name = "Omar Ali",
                Gender = Gender.Male,
                Email = "omar.ali@example.com",
                MobileNo = "+201223456789",
                Birthdate = new DateTime(2001, 3, 10),
                IsGraduated = false,
                TrackId = 3
            }
        };
    }

    public List<Trainee> GetAll() => _trainees;

    public Trainee? GetById(int id) => _trainees.FirstOrDefault(t => t.Id == id);

    public void Add(Trainee trainee)
    {
        trainee.Id = _nextId++;
        _trainees.Add(trainee);
    }

    public void Update(Trainee trainee)
    {
        var existing = GetById(trainee.Id);
        if (existing is null) return;

        existing.Name = trainee.Name;
        existing.Gender = trainee.Gender;
        existing.Email = trainee.Email;
        existing.MobileNo = trainee.MobileNo;
        existing.Birthdate = trainee.Birthdate;
        existing.IsGraduated = trainee.IsGraduated;
        existing.TrackId = trainee.TrackId;
    }

    public void Delete(int id)
    {
        var trainee = GetById(id);
        if (trainee is not null)
            _trainees.Remove(trainee);
    }
}
