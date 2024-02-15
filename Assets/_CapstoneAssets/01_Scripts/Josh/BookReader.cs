using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using BookCurlPro;
using Language.Lua;

public class BookReader : MonoBehaviour
{
    TextAsset bookText; // the text of our book

    // our page
    string leftPage, rightPage;
    string bookAsString; // the book, stored as a string.

    // our starting character
    [SerializeField] int startingCharacter, charactersPerPage, leftPageNumber;

    // setup our text outputs
    [SerializeField] UnityEngine.UI.Text leftPageText, rightPageText;

    public enum Books
    {
        dracula, pride
    }

    // our book
    public Books book;

    void Start()
    {
        AssignBook();
    }

    // assign our book
    void AssignBook()
    {
        switch (book)
        {
            case Books.dracula:
                bookText = Resources.Load<TextAsset>("Books/dracula");
                break;

            case Books.pride:
                bookText = Resources.Load<TextAsset>("Books/pride-and-prejudice");
                break;
        }

        // then turn the book into a string
        bookAsString = bookText.text;

        BuildPage();
    }

    void BuildPage()
    {
        // reset our left page
        leftPage = "";
        int remainingCharacters = 0;

        // loop through our book, starting from our starting character, until we have hit our maximum characters per page
        for (int character = startingCharacter; character < startingCharacter + charactersPerPage; character++)
        {
            // if we are approaching the end of this page, break from the loop and stop counting
            if (character >= startingCharacter + charactersPerPage - 30 && bookAsString[character] == ' ')
            {
                // get the remaining characters and save them before we do our next loop
                remainingCharacters = (startingCharacter + charactersPerPage) - character;
                Debug.Log(remainingCharacters);
                break;
            }

            // if we find a linebreak in our text, add a space instead
            if (bookAsString[character] == '\n' && leftPageNumber >= 2)
                leftPage += " ";
            else
            // add to our page
            leftPage += bookAsString[character];
        }

        // reset our right page
        rightPage = "";

        // starting from the next page, build the right page
        for (int character = startingCharacter + (charactersPerPage - remainingCharacters); character < startingCharacter + charactersPerPage * 2; character++)
        {
            // if we are approaching the end of this page, break from the loop and stop counting
            if (character >= startingCharacter + charactersPerPage * 2 - 30 && bookAsString[character] == ' ')
                break;

            // if we find a linebreak in our text, add a space instead
            if (bookAsString[character] == '\n' && leftPageNumber >= 2)
                rightPage += " ";
            else
                // add to our page
                rightPage += bookAsString[character];
        }

        // write the page
        leftPageText.text = leftPage;
        rightPageText.text = rightPage;
    }

    // Update is called once per frame
    void Update()
    {
        // get our first line and print on space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BuildPage();
        }

        // advance and go back
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // add to our start character
            startingCharacter += charactersPerPage * 2;
            leftPageNumber += 2;
            BuildPage();
        }        
       
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // subtract from our start character
            startingCharacter -= charactersPerPage * 2;
            leftPageNumber = leftPageNumber <= 0 ? 0 : leftPageNumber;
            startingCharacter = startingCharacter <= 0 ? 0 : startingCharacter;
            BuildPage();
        }
    }
}
