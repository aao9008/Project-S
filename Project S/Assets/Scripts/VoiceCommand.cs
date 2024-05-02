using UnityEngine;
using UnityEngine.Windows.Speech;
using MixedReality.Toolkit;
using MixedReality.Toolkit.Subsystems;
using System.Collections;
using System.Diagnostics;
using UnityEngine.UI;

public class VoiceCommand : MonoBehaviour
{
    KeywordRecognizer recognizer;
    TextToSpeechSubsystem textToSpeechSubsystem;
    public AudioSource myAudio;
    public AudioSource processEndSound;
    public Test dictationManager;
    public PositionSlateInMiddle positionSlateScript;
    public GameObject aiModel;
    public GameObject notesPanel;
    public GameObject notesForm;

    void Start()
    {
        // Set the GameObject to be initially inactive
        aiModel.SetActive(false);
        notesPanel.SetActive(false);
        notesForm.SetActive(false);

        // Initialize KeywordRecognizer with the keyword "Socius"
        recognizer = new KeywordRecognizer(new string[] { "Socius", "Socius dismiss", "test", "Socius take notes" });

        // Register a callback function to be called when the keyword is recognized
        recognizer.OnPhraseRecognized += OnPhraseRecognized;


       

        // Start recognition
        recognizer.Start();

        

        // Initialize the TextToSpeech subsystem
        textToSpeechSubsystem = XRSubsystemHelpers.GetFirstRunningSubsystem<TextToSpeechSubsystem>();

        // Get reference to the Test script
       
    }



    void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        // Check if the recognized phrase matches the keyword "Socius"
        if (args.text == "test")
        {
            // Make the sphere model appear (enable its GameObject)
            aiModel.SetActive(true);

            // Speak "hello" using SpeechSynthesizer
            // Speak message
            // Get the first running text to speech subsystem.
            //TextToSpeechSubsystem textToSpeechSubsystem = XRSubsystemHelpers.GetFirstRunningSubsystem<TextToSpeechSubsystem>();
            textToSpeechSubsystem.TrySpeak("Hello, my name is Socius. I will be your first assistant for this procedure. How can I be of assistance?", myAudio);
        }
        else if (args.text == "Socius dismiss")
        {
            // Make the sphere model disappear (disable its GameObject)
            aiModel.SetActive(false);
            UnityEngine.Debug.Log("good-bye");
        }
        // Check if the recognized phrase matches the keyword "Socius"
        else if (args.text == "Socius take notes")
        {
            positionSlateScript.SetSlatePositionAndRotation();

            // Dispaly notes panel
            notesPanel.SetActive(true);

            // Stop the Phrase recognition system
            PhraseRecognitionSystem.Shutdown();

            // Speak "Taking notes" using TextToSpeech
            textToSpeechSubsystem.TrySpeak("Taking notes", myAudio);

            // Start dictation when "take notes" command is recognized
            dictationManager.StartRecognition();
        }
    }

    // This function will be called when RecognitionFinished event fires
    public void RestartCommandsSystem()
    {
        // Enable voice commands
        PhraseRecognitionSystem.Restart();
        processEndSound.Play();
    }

    // This funciton will be called when StartRecognition event fires
    public void StopCommandsSystem()
    {
        // Disable voice commands
        PhraseRecognitionSystem.Shutdown();
    }

    // This function simulates form transmission to EPIC
    public void SubmitForm()
    {
        // Hide the form
        notesForm.SetActive(false);

        // Activate the loading spinner after a delay
        StartCoroutine(ActivateLoadingSpinnerAfterDelay());
    }

    public GameObject canvas;
    

    IEnumerator ActivateLoadingSpinnerAfterDelay()
    {
        // Wait for 1 second (adjust as needed)
        yield return new WaitForSeconds(1f);

        // Activate the loading spinner
        canvas.SetActive(true);

        yield return new WaitForSeconds(3f);

        canvas.SetActive(false);

        textToSpeechSubsystem.TrySpeak("Notes successfully submitted to Imagine Hive", myAudio);
    }
}

