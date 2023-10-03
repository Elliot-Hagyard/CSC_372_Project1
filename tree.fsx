
type Tree = { 
    Value : int
    left : Root
    right : Root
}
and Root = 
    | Tree of Tree
    | Empty

let rec delete el tree : Root = 
    match tree with
    | Empty -> Empty
    | Tree t -> 
        if t.Value = el then 
            match (t.left, t.right) with
            | (Empty, _) -> t.right
            | (_, Empty) -> t.left
            | (Tree l, Tree r) -> Tree {
                Value = l.Value; 
                right = Tree r;
                left = delete l.Value t.left
            }
        else if t.Value < el then Tree { t with right = delete el t.right }
        else Tree { t with left = delete el t.left }



let rec insert el tree = 
    match tree with
    | Tree t ->
        if t.Value = el then tree
        else if t.Value < el then Tree { t with right = insert el t.right}
        else Tree { t with left = insert el t.left}
    | Empty -> Tree {Value = el; left = Empty; right = Empty;}

let rec addToTree xs t = 
    match xs with
    | [] -> t
    | [x] -> insert x t
    | l::ls -> insert l (addToTree ls t)


let rec postOrderTraversal tree =
    match tree with
    | Empty -> []
    | Tree t -> postOrderTraversal t.left @ postOrderTraversal t.right @ [t.Value]

let rec height tree = 
    match tree with
    | Empty -> 0
    | Tree t -> 
        match (t.left, t.right) with 
        | (Empty, Empty) -> 0
        | (_, _) -> 1 + max (height t.left) (height t.right)

let rec depth el tree = 
    match tree with
    | Empty -> -1
    | Tree t -> 
        if t.Value = el then 0
        else if t.Value < el then 1 + depth el t.right 
        else 1 + depth el t.left


(* Basic tests*)
// TODO add more tests (look into FsCheck)


let a = Tree {
    Value = 1   
    left = Empty
    right = Empty
}
let example_list = [10; 5; 4; 20; 30; 15; 12; 9]
let c = addToTree example_list Empty
let mutable b = insert -2 a
b <- insert 3 b
b <- insert 2 b
b <- insert 4 b
b <- delete 3 b
height b
depth 2 b

printfn $"{b}"
printfn $"{c}"
postOrderTraversal c 
