public class ActivityCreateViewModel
{
    public EventViewModel Event { get; set; }
    // other properties

    public ActivityCreateViewModel()
    {
        Event = new EventViewModel(); // Initialize Event property
    }
    public int NumberOfActivities { get; set; }
    public List<ActivityViewModel> Activities { get; set; }
}
