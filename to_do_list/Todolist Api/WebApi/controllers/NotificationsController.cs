[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationsController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    // Получить все уведомления
    [HttpGet]
    public IActionResult GetAllNotifications()
    {
        var notifications = _notificationService.GetAllNotifications();
        return Ok(notifications);
    }

    // Получить уведомление по ID
    [HttpGet("{id}")]
    public IActionResult GetNotificationById(int id)
    {
        var notification = _notificationService.GetNotificationById(id);
        if (notification == null)
            return NotFound();

        return Ok(notification);
    }

    // Создать новое уведомление
    [HttpPost]
    public IActionResult CreateNotification([FromBody] Notification notification)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _notificationService.AddNotification(notification);
        return CreatedAtAction(nameof(GetNotificationById), new { id = notification.Id }, notification);
    }

    // Обновить статус отправленного уведомления
    [HttpPatch("{id}/mark-sent")]
    public IActionResult MarkNotificationAsSent(int id)
    {
        var notification = _notificationService.MarkAsSent(id);
        if (notification == null)
            return NotFound();

        return Ok(notification);
    }
}
