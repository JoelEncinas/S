using UnityEngine.SceneManagement;

public static class SceneManagerScript 
{
    public static void ResetGame()
    {
        SceneManager.LoadScene("Loading");
        SceneManager.LoadScene("Mission01");
    }
}
