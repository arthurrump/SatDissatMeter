module App

open Browser
open System
open Elmish
open Lit

type Model =
    { Sat: int
      Dissat: int }

type Message =
    | SetSat of int
    | SetDissat of int

let init () =
    { Sat = 5; Dissat = 5 }

let update msg model =
    match msg with
    | SetSat sat -> { model with Sat = sat }
    | SetDissat dissat -> { model with Dissat = dissat }

let range name value onChange =
    html $"""
        <input type="range" id="{name}" name="{name}" min="1" max="10" value={value}
               @change={Ev (fun ev -> onChange (Int32.Parse ev.target.Value))} />
    """

let view model dispatch =
    let smileys = svg $"""
        <symbol id="sad" width="20" height="20">
            <circle cx="10" cy="10" r="10" fill="#ee5555" />
            <path fill="#000000" d="M6.5 8c-0.827 0-1.5-0.673-1.5-1.5s0.673-1.5 1.5-1.5 1.5 0.673 1.5 1.5-0.673 1.5-1.5 1.5zM6.5 6c-0.276 0-0.5 0.224-0.5 0.5s0.224 0.5 0.5 0.5 0.5-0.224 0.5-0.5-0.224-0.5-0.5-0.5z"></path>
            <path fill="#000000" d="M12.5 8c-0.827 0-1.5-0.673-1.5-1.5s0.673-1.5 1.5-1.5 1.5 0.673 1.5 1.5-0.673 1.5-1.5 1.5zM12.5 6c-0.276 0-0.5 0.224-0.5 0.5s0.224 0.5 0.5 0.5 0.5-0.224 0.5-0.5-0.224-0.5-0.5-0.5z"></path>
            <path fill="#000000" d="M5.499 15c-0.105 0-0.21-0.033-0.3-0.1-0.221-0.166-0.265-0.479-0.099-0.7 0.502-0.668 1.159-1.221 1.901-1.6 0.778-0.398 1.619-0.599 2.499-0.599s1.721 0.202 2.499 0.599c0.742 0.379 1.399 0.932 1.901 1.6 0.166 0.221 0.121 0.534-0.099 0.7s-0.534 0.121-0.7-0.099c-0.859-1.144-2.172-1.8-3.601-1.8s-2.741 0.656-3.601 1.8c-0.098 0.131-0.248 0.2-0.4 0.2z"></path>
        </symbol>
        <symbol id="happy" width="20" height="20">
            <circle cx="10" cy="10" r="10" fill="#55ee55" />
            <path fill="#000000" d="M6.5 8c-0.827 0-1.5-0.673-1.5-1.5s0.673-1.5 1.5-1.5 1.5 0.673 1.5 1.5-0.673 1.5-1.5 1.5zM6.5 6c-0.276 0-0.5 0.224-0.5 0.5s0.224 0.5 0.5 0.5 0.5-0.224 0.5-0.5-0.224-0.5-0.5-0.5z"></path>
            <path fill="#000000" d="M12.5 8c-0.827 0-1.5-0.673-1.5-1.5s0.673-1.5 1.5-1.5 1.5 0.673 1.5 1.5-0.673 1.5-1.5 1.5zM12.5 6c-0.276 0-0.5 0.224-0.5 0.5s0.224 0.5 0.5 0.5 0.5-0.224 0.5-0.5-0.224-0.5-0.5-0.5z"></path>
            <path fill="#000000" d="M9.5 17c-3.584 0-6.5-2.916-6.5-6.5 0-0.276 0.224-0.5 0.5-0.5s0.5 0.224 0.5 0.5c0 3.033 2.467 5.5 5.5 5.5s5.5-2.467 5.5-5.5c0-0.276 0.224-0.5 0.5-0.5s0.5 0.224 0.5 0.5c0 3.584-2.916 6.5-6.5 6.5z"></path>
        </symbol>
    """

    let happy cX cY = svg $"""<use href="#happy" x="{cX - 10}" y="{cY - 10}" />"""
    let sad cX cY = svg $"""<use href="#sad" x="{cX - 10}" y="{cY - 10}" />"""

    let line =
        let divider i = svg $"""<line x1="{i * 15}" y1="25" x2="{i * 15}" y2="35" stroke="black" stroke-width="2" />"""
        svg $"""
            <line x1="15" y1="30" x2="150" y2="30" stroke="black" stroke-width="2" />
            {[ for i in 1..10 -> divider i ]}
        """

    html $"""
        <div>
            <svg id="image" width="660" height="240" viewBox="0 0 165 60">
                {smileys}
                {line}
                {happy (model.Sat * 15) 10}
                {sad (model.Dissat * 15) 50}
            </svg>
            <div>
                {range "sat" model.Sat (SetSat >> dispatch)}
                <label for="sat">Sat: {Lit.ofInt model.Sat}</label>
            </div>
            <div>
                {range "dissat" model.Dissat (SetDissat >> dispatch)}
                <label for="dissat">Dissat: {Lit.ofInt model.Dissat}</label>
            </div>

        </div>
    """

open Lit.Elmish
open Lit.Elmish.HMR

Program.mkSimple init update view
|> Program.withLit "app-container"
|> Program.run
