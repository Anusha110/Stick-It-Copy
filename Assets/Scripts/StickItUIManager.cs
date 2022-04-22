using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StickItUIManager : MonoBehaviour
{
    
    [SerializeField] private Sprite stickit_1, stickit_2, stickit_3;

    [SerializeField] private TextMeshProUGUI instructionText;
    [SerializeField] private Image intructionImage;

    [SerializeField] private GameObject forwardBtn;
    [SerializeField] private GameObject backwardBtn;
    [SerializeField] private GameObject startGameBtn;

    [SerializeField] private GameObject dots;

    private int currentPageIndex = 0;


    private struct InstructionPage
    {
        public string InstructionText;
        public Sprite InstructionImage;
        public bool Forward;
        public bool Backward;
        public bool StartGame;
        public bool ShowDots;

        public void SetInstructionPage(string instructionText, Sprite instructionImage, bool forward, bool backward, bool startGame, bool showDots)
        {
            InstructionText = instructionText;
            InstructionImage = instructionImage;
            Forward = forward;
            Backward = backward;
            StartGame = startGame;
            ShowDots = showDots;
        }

    }

    private InstructionPage[] instructionPages = { new InstructionPage(), new InstructionPage(), new InstructionPage(), new InstructionPage(), new InstructionPage() };

    void Awake()
    {

        instructionPages[0].SetInstructionPage("Press on the screen to launch the ball to the next trampoline. The longer you press, the more distance it jumps.", stickit_1, true, false, false, true);


        instructionPages[1].SetInstructionPage("Try to land ball at the center of trampoline to score high. You will lose a life if the ball misses a trampoline.", stickit_2, true, true, false, true);


        instructionPages[2].SetInstructionPage("The ball sticks to the trampoline when it lands. You will lose a life, if the ball stays for more than 5 seconds on a trampoline.", stickit_3, false, true, true, false);

        LoadPage();

    }


    public void GoForward()
    {
        currentPageIndex += 1;
        LoadPage();
    }

    public void GoBack()
    {
        currentPageIndex -= 1;
        LoadPage();
    }

    public void StartGame()
    {
        // Load gameplay scene
    }

    public void LoadPage()
    {
        InstructionPage currentPage = instructionPages[currentPageIndex];

        instructionText.text = currentPage.InstructionText;
        intructionImage.sprite = currentPage.InstructionImage;
        forwardBtn.SetActive(currentPage.Forward);
        backwardBtn.SetActive(currentPage.Backward);
        startGameBtn.SetActive(currentPage.StartGame);

        TextMeshProUGUI firstDot = dots.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
        TextMeshProUGUI secondDot = dots.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>();


        if (currentPage.ShowDots)
        {

            if (currentPageIndex == 0)
            {
                firstDot.color = Color.white;
                secondDot.color = Color.black;
            }
            else
            {
                firstDot.color = Color.black;
                secondDot.color = Color.white;
            }
        }

    }


}
