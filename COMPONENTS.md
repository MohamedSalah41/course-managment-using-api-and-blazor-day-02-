# Shared Components — How Each File Works

---

## 1. `ConfirmDialog.razor`

### What it does
A popup modal that asks the user "are you sure?" before deleting something.  
It only appears when `IsVisible` is `true`. When visible, it blocks the page with a dark overlay behind it.

### The code
```razor
@if (IsVisible)
{
    <div class="modal fade show d-block" style="background-color: rgba(0,0,0,0.5);">
        <div class="modal-header bg-danger text-white">
            <h5>@Title</h5>
        </div>
        <div class="modal-body">
            <p>@Message</p>
        </div>
        <div class="modal-footer">
            <button @onclick="Cancel">Cancel</button>
            <button @onclick="Confirm">Delete</button>
        </div>
    </div>
}

@code {
    [Parameter] public string Title { get; set; }
    [Parameter] public string Message { get; set; }
    [Parameter] public bool IsVisible { get; set; }
    [Parameter] public EventCallback OnConfirm { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }
}
```

### When it's triggered
- Placed at the **bottom of `TrackList.razor` and `TraineeList.razor`**
- Stays hidden (`IsVisible = false`) until the user clicks a **Delete** button on a row
- Clicking Delete → sets `IsVisible = true` → modal appears
- Clicking **Confirm** → fires `OnConfirm` → parent deletes the item and sets `IsVisible = false`
- Clicking **Cancel** → fires `OnCancel` → parent sets `IsVisible = false` → modal disappears

```
User clicks Delete button
        ↓
AskDelete() sets _showDialog = true
        ↓
ConfirmDialog becomes visible
        ↓
User clicks "Delete"  →  ExecuteDelete() runs  →  item deleted
User clicks "Cancel"  →  CancelDelete() runs   →  nothing deleted
```

---

## 2. `LoadingSpinner.razor`

### What it does
Shows a spinning circle while data is being loaded.  
When `IsLoading = false`, it renders **nothing at all** — no empty space, no placeholder.

### The code
```razor
@if (IsLoading)
{
    <div class="d-flex justify-content-center py-5">
        <div class="spinner-border text-primary" style="width: @Size; height: @Size;"></div>
        @if (!string.IsNullOrWhiteSpace(Message))
        {
            <span>@Message</span>
        }
    </div>
}

@code {
    [Parameter] public bool IsLoading { get; set; } = true;
    [Parameter] public string Message { get; set; } = "Loading...";
    [Parameter] public string Size { get; set; } = "3rem";
}
```

### When it's triggered
- Used on any page that loads data asynchronously (e.g., fetching from an API)
- You place it at the top of a page and bind `IsLoading` to a bool field
- When the page starts → `_isLoading = true` → spinner shows
- When data finishes loading → `_isLoading = false` → spinner disappears, content shows

```razor
@* Example usage in a page *@
<LoadingSpinner IsLoading="_isLoading" Message="Loading trainees..." Size="3rem" />

@code {
    private bool _isLoading = true;

    protected override async Task OnInitializedAsync()
    {
        _trainees = await TraineeService.GetAllAsync();
        _isLoading = false;   // ← spinner disappears here
    }
}
```

---

## 3. `PageHeading.razor`

### What it does
A reusable styled page header — renders an `<h1>` with an optional icon and subtitle.  
Named `PageHeading` (not `PageTitle`) to avoid clashing with Blazor's built-in `<PageTitle>` tag.

### The code
```razor
<div class="d-flex align-items-center mb-4 border-bottom pb-2">
    @if (!string.IsNullOrWhiteSpace(Icon))
    {
        <span class="@Icon fs-3 me-3 text-primary"></span>
    }
    <div>
        <h1 class="mb-0 fs-3 fw-bold">@Title</h1>
        @if (!string.IsNullOrWhiteSpace(Subtitle))
        {
            <p class="text-muted mb-0 small">@Subtitle</p>
        }
    </div>
</div>

@code {
    [Parameter, EditorRequired] public string Title { get; set; }
    [Parameter] public string? Subtitle { get; set; }
    [Parameter] public string? Icon { get; set; }
}
```

### When it's triggered
- Placed at the **top of every page** (TrackList, TrackCreate, TraineeList, etc.)
- Renders immediately when the page loads — no condition, always visible
- `Title` is required (`EditorRequired`) — you'll get a warning if you forget it
- `Subtitle` and `Icon` are optional — if not passed, those sections simply don't render

```razor
@* Example usage *@
<PageHeading Title="Trainees"
             Subtitle="Manage all enrolled trainees"
             Icon="bi bi-people-fill" />
```

```
Page loads
    ↓
PageHeading renders immediately
    ↓
Shows icon + h1 title + subtitle (if provided)
```

---

## 4. `ValidationSummaryPanel.razor`

### What it does
A styled red alert box that lists **all form validation errors at once**.  
It wraps Blazor's built-in `<ValidationSummary>` inside a Bootstrap `alert-danger` box with a header line.  
Needs **no parameters** — it automatically reads errors from the `<EditForm>` it's placed inside.

### The code
```razor
<div class="alert alert-danger py-2 px-3" role="alert">
    <div class="d-flex align-items-center mb-1">
        <span class="bi bi-x-circle-fill me-2"></span>
        <strong>Please fix the following errors:</strong>
    </div>
    <ValidationSummary />
</div>

@code {
    // No parameters — ValidationSummary reads from the nearest EditContext automatically
}
```

### When it's triggered
- Placed **inside an `<EditForm>`** on Create and Edit pages
- Hidden by default — Blazor only renders `<ValidationSummary>` content when there are actual errors
- Triggered when the user clicks **Submit** on an invalid form
- As the user fixes each field, the corresponding error disappears from the list automatically

```razor
@* Example usage inside a form *@
<EditForm Model="_track" OnValidSubmit="HandleSubmit">
    <DataAnnotationsValidator />
    <ValidationSummaryPanel />   @* ← shows all errors here *@

    <InputText @bind-Value="_track.Name" />
    <ValidationMessage For="() => _track.Name" />  @* ← shows per-field error *@

    <button type="submit">Save</button>
</EditForm>
```

```
User clicks Submit with empty form
        ↓
DataAnnotationsValidator checks all fields
        ↓
Errors found → ValidationSummaryPanel shows red box with all error messages
        ↓
User fixes a field → that error disappears from the box
        ↓
All fields valid → red box disappears → OnValidSubmit fires
```

---

## Summary Table

| Component | Triggered When | Controlled By | Required Params |
|---|---|---|---|
| `ConfirmDialog` | User clicks Delete button | `IsVisible` bool in parent | `Title`, `Message`, `OnConfirm`, `OnCancel` |
| `LoadingSpinner` | Page starts loading data | `IsLoading` bool in parent | none (defaults work) |
| `PageHeading` | Page loads (always visible) | always renders | `Title` |
| `ValidationSummaryPanel` | Form submitted with errors | Blazor's EditContext | none |
