using UnityEngine;

public class MVC : MonoBehaviour
{
    private Application app;

    public Application App
    {
        // ?? syntax: checks if left is null. If left is not null, right is not executed.
        get { return app ?? (app = FindObjectOfType<Application>()); }
    }
}
