# Assignment – Working with Real Data (Blazor WASM + ASP.NET Core API)

---

## Task 1 – Create the ASP.NET Core Web API Project

- [ ] Add a new ASP.NET Core Web API project to the solution (e.g. `TraineesAPI`)
- [ ] Add it to the existing `.slnx` solution file
- [ ] Install required NuGet packages:
  - `Microsoft.EntityFrameworkCore.SqlServer` (or SQLite for local dev)
  - `Microsoft.EntityFrameworkCore.Tools`
  - `Microsoft.EntityFrameworkCore.Design`

---

## Task 2 – Define Models with DataAnnotations in the API Project

- [ ] Create `Models/Track.cs` with properties: `Id`, `Name`, `Description`
- [ ] Create `Models/Trainee.cs` with properties: `Id`, `Name`, `Gender`, `Email`, `MobileNo`, `Birthdate`, `IsGraduated`, `TrackId`
- [ ] Create `Models/Gender.cs` enum: `Male`, `Female`
- [ ] Apply DataAnnotations on both models:
  - `[Required]`, `[StringLength]` on `Name`
  - `[StringLength]` on `Description`
  - `[Required]`, `[EmailAddress]` on `Email`
  - `[Required]`, `[Phone]` on `MobileNo`
  - `[Required]` on `Gender` and `Birthdate`

---

## Task 3 – Set Up the Database with Entity Framework Core

- [ ] Create `Data/AppDbContext.cs` inheriting from `DbContext`
- [ ] Add `DbSet<Track>` and `DbSet<Trainee>` to the context
- [ ] Configure the connection string in `appsettings.json`
- [ ] Register `AppDbContext` in `Program.cs`
- [ ] Run `Add-Migration InitialCreate` and `Update-Database` to create the DB

---

## Task 4 – Implement the Repository Pattern

- [ ] Create `Repositories/ITrackRepository.cs` interface with methods:
  - `GetAllAsync()`, `GetByIdAsync(int id)`, `AddAsync(Track)`, `UpdateAsync(Track)`, `DeleteAsync(int id)`
- [ ] Create `Repositories/TrackRepository.cs` implementing the interface using `AppDbContext`
- [ ] Create `Repositories/ITraineeRepository.cs` interface with the same CRUD methods for `Trainee`
- [ ] Create `Repositories/TraineeRepository.cs` implementing the interface
- [ ] Register both repositories in `Program.cs` (scoped lifetime)

---

## Task 5 – Build the Tracks API Controller

- [ ] Create `Controllers/TracksController.cs`
- [ ] Inject `ITrackRepository` via constructor
- [ ] Implement endpoints:
  - `GET /api/tracks` – return all tracks
  - `GET /api/tracks/{id}` – return single track (404 if not found)
  - `POST /api/tracks` – create track (validate model, return 201)
  - `PUT /api/tracks/{id}` – update track (validate model, return 204)
  - `DELETE /api/tracks/{id}` – delete track (return 204)
- [ ] Use `[ApiController]` and `[Route("api/[controller]")]` attributes

---

## Task 6 – Build the Trainees API Controller

- [ ] Create `Controllers/TraineesController.cs`
- [ ] Inject `ITraineeRepository` via constructor
- [ ] Implement endpoints:
  - `GET /api/trainees` – return all trainees
  - `GET /api/trainees/{id}` – return single trainee (404 if not found)
  - `POST /api/trainees` – create trainee (validate model, return 201)
  - `PUT /api/trainees/{id}` – update trainee (validate model, return 204)
  - `DELETE /api/trainees/{id}` – delete trainee (return 204)
- [ ] Use `[ApiController]` and `[Route("api/[controller]")]` attributes

---

## Task 7 – Configure CORS in the API

- [ ] Add CORS policy in `Program.cs` to allow requests from the Blazor WASM origin (e.g. `https://localhost:7xxx`)
- [ ] Apply the CORS policy using `app.UseCors(...)`

---

## Task 8 – Configure HttpClientFactory in the Blazor WASM Project

- [ ] In the Blazor project's `Program.cs`, register a named or typed `HttpClient` pointing to the API base URL
- [ ] Use `builder.Services.AddHttpClient(...)` with the API base address
- [ ] Remove or disable the old in-memory `TrackService` and `TraineeService` registrations

---

## Task 9 – Create API Client Services in the Blazor Project

- [ ] Create `Services/TrackApiService.cs`:
  - Inject `HttpClient`
  - Implement: `GetAllAsync()`, `GetByIdAsync(int)`, `CreateAsync(Track)`, `UpdateAsync(Track)`, `DeleteAsync(int)`
  - Use `GetFromJsonAsync`, `PostAsJsonAsync`, `PutAsJsonAsync`, `DeleteAsync`
- [ ] Create `Services/TraineeApiService.cs` with the same pattern for `Trainee`
- [ ] Register both services in `Program.cs`

---

## Task 10 – Update Track Pages to Use the API

- [ ] Update `TrackList.razor` – call `TrackApiService.GetAllAsync()` on load
- [ ] Update `TrackCreate.razor` – call `TrackApiService.CreateAsync(track)` on valid submit
- [ ] Update `TrackEdit.razor` – load via `GetByIdAsync`, save via `UpdateAsync`
- [ ] Update `TrackDetails.razor` – load via `GetByIdAsync`
- [ ] Update delete in `TrackList.razor` – call `DeleteAsync`, refresh list

---

## Task 11 – Update Trainee Pages to Use the API

- [ ] Update `TraineeList.razor` – call `TraineeApiService.GetAllAsync()` on load
- [ ] Update `TraineeCreate.razor` – call `TraineeApiService.CreateAsync(trainee)` on valid submit
- [ ] Update `TraineeEdit.razor` – load via `GetByIdAsync`, save via `UpdateAsync`
- [ ] Update `TraineeDetails.razor` – load via `GetByIdAsync`
- [ ] Update delete in `TraineeList.razor` – call `DeleteAsync`, refresh list

---

## Task 12 – Validation and Error Display

- [ ] Ensure every form has `<DataAnnotationsValidator>` and `<ValidationSummary>` inside `<EditForm>`
- [ ] Add `<ValidationMessage For="...">` under each individual field
- [ ] Handle API error responses (e.g. 400, 404, 500) in the Blazor pages:
  - Show a user-friendly error message when the API returns an error
  - Display loading state using `LoadingSpinner` component while awaiting API calls
- [ ] Test submitting invalid data and verify validation messages appear correctly

---

## Task 13 – End-to-End Testing

- [ ] Run both projects (API + Blazor WASM) simultaneously
- [ ] Test full CRUD flow for Tracks via the UI
- [ ] Test full CRUD flow for Trainees via the UI
- [ ] Verify data persists in the database across page refreshes
- [ ] Verify all validation messages display correctly on invalid input
