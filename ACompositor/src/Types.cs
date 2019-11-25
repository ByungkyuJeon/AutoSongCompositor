using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACompositor.src
{
    /// <summary>
    /// song form
    /// </summary>
    public enum FormType
    {
        Intro, Verse, Verse2, Hook, Bridge, Interlude, Outro
    }

    /// <summary>
    /// song jengre
    /// </summary>
    public enum Jengre
    {
        NewAge,
        Normal
    }

    public enum ReplChord
    {
        Tonic, SubDominant2, Tonic2, SubDominant, Dominant, Tonic3, Dominant2
    }

    /// <summary>
    /// Enum for Notes
    /// </summary>
    public enum Note
    {
        C, Cu, D, Du, E, F, Fu, G, Gu, A, Au, B,

        C1, Cu1, D1, Du1, E1, F1, Fu1, G1, Gu1, A1, Au1, B1,

        C2, Cu2, D2, Du2, E2, F2, Fu2, G2, Gu2, A2, Au2, B2,

        C3, Cu3, D3, Du3, E3, F3, Fu3, G3, Gu3, A3, Au3, B3,

        C4, Cu4, D4, Du4, E4, F4, Fu4, G4, Gu4, A4, Au4, B4,

        C5, Cu5, D5, Du5, E5, F5, Fu5, G5, Gu5, A5, Au5, B5,

        C6, Cu6, D6, Du6, E6, F6, Fu6, G6, Gu6, A6, Au6, B6,

        C7, Cu7, D7, Du7, E7, F7, Fu7, G7, Gu7, A7, Au7, B7,

        NULL = -1
    }

    /// <summary>
    /// enum for scale
    /// </summary>
    public enum Scale
    {
        Major, // 34, 78 반음
        Minor, // 23, 56 반음

        NULL
    }

    /// <summary>
    /// Enum for Codes
    /// </summary>
    public enum ChordNote
    {
        C_Major, C_Minor, C_Sus4, C_Dim, C_M7, C_M6, C_M2_9,

        D_Major, D_Minor, D_Sus4, D_Dim, D_M7, D_M6, D_M2_9,

        E_Major, E_Minor, E_Sus4, E_Dim, E_M7, E_M6, E_M2_9,

        F_Major, F_Minor, F_Sus4, F_Dim, F_M7, F_M6, F_M2_9,

        G_Major, G_Minor, G_Sus4, G_Dim, G_M7, G_M6, G_M2_9,

        A_Major, A_Minor, A_Sus4, A_Dim, A_M7, A_M6, A_M2_9,

        B_Major, B_Minor, B_Sus4, B_Dim, B_M7, B_M6, B_M2_9,

        NULL
    }

    /// <summary>
    /// Enum for Accompanies
    /// </summary>
    public enum Accomp
    {
        _4Beat,
        Arpeggio
    }

    /// <summary>
    /// kind of variation
    /// </summary>
    public enum Variation
    {
        Origin, Extend, Shrink, Octaviation, Tailing, Newition,

        NULL
    }

    /// <summary>
    /// Mellody Tones
    /// </summary>
    public enum MellTone
    {
        Tone, Tension, Avoid
    }
}