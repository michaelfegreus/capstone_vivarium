using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using BookCurlPro;
using Language.Lua;

public class BookReader : MonoBehaviour
{
    TextAsset bookText; // the text of our book

    // the line our page starts on
    [SerializeField] int firstLine;
    // how many lines per page
    [SerializeField] int linesPerPage;
    // characters per line
    [SerializeField] int charactersPerLine;
    int firstChar; // the current page we are on in the book

    // our page
    [SerializeField] string page;

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

        BuildPage();
    }

    void BuildPage()
    {
        page = "";

        // start with lines
        for (int i = firstLine; i < linesPerPage; i++) 
        { 
            // then do each line individually
            for (int j = 0; j < charactersPerLine; j++)
            { // get all of our characters per each line
                page += bookText.ToString()[j+firstLine];
            }
        }

        // log the page
        Debug.Log(page);
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
            firstLine += linesPerPage;
            firstChar += charactersPerLine * linesPerPage;
            BuildPage();
        }        
       
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            firstChar -= charactersPerLine * linesPerPage;

            if (firstLine - linesPerPage >= 0)
                firstLine -= linesPerPage;

            if (firstLine - linesPerPage < 0)
                firstLine = 0;

            BuildPage();
        }
    }
}
