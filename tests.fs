open Expecto

open FPGA.PGA2D

// https://bivector.net/tools.html?p=2&q=0&r=1
[<Tests>]
let testVecs =
    let a = vec 1 2 3
    let b = vec 4 5 6
    let c = vec 0 1 0
    let d = vec -1 0 -1
    let e = vec -1 -2 -3

    let test name case expect str =
        testCase name <| fun () ->
            Expect.equal case expect str
            Expect.equal $"{case}" str str

    testList "Vector tests" [
        testList "Dual" [
            test $"a* = ({a})*" !*a (bivec 3 -2  1) "3e01 - 2e02 + e12"
            test $"b* = ({b})*" !*b (bivec 6 -5  4) "6e01 - 5e02 + 4e12"
            test $"c* = ({c})*" !*c (bivec 0 -1  0) "-e02"
            test $"d* = ({d})*" !*d (bivec -1 0 -1) "-e01 - e12"
            test $"e* = ({e})*" !*e (bivec -3 2 -1) "-3e01 + 2e02 - e12"
        ]
        testList "Outer Product" [
            testCase $"a∧b = ({a})∧({b})" <| fun () ->
                let case   = a .^. b
                let expect = bivec -3 -6 -3
                Expect.equal case expect ""
                Expect.equal $"{case}" "-3e01 - 6e02 - 3e12" ""
            testCase $"a∧c = ({a})∧({c})" <| fun () ->
                let case   = a .^. c
                let expect = bivec 1 0 -3
                Expect.equal case expect ""
                Expect.equal $"{case}" "e01 - 3e12" ""
            testCase $"a∧d = ({a})∧({d})" <| fun () ->
                let case   = a .^. d
                let expect = bivec 2 2 -2
                Expect.equal case expect ""
                Expect.equal $"{case}" "2e01 + 2e02 - 2e12" ""
            testCase $"a∧e = ({a})∧({e})" <| fun () ->
                let case   = a .^. e
                let expect = bivec 0 0 0
                Expect.equal case expect ""
                Expect.equal $"{case}" "0" ""
        ]
    ]

[<EntryPoint>]
let runTests args =
    runTestsInAssemblyWithCLIArgs [] [||]
