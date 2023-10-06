
type Tree = { 
    value : int
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
        if t.value = el then 
            match (t.left, t.right) with
            | (Empty, _) -> t.right
            | (_, Empty) -> t.left
            | (Tree l, Tree r) -> Tree {
                value = l.value; 
                right = Tree r;
                left = delete l.value t.left
            }
        else if t.value < el then Tree { t with right = delete el t.right }
        else Tree { t with left = delete el t.left }



let rec insert el tree = 
    match tree with
    | Tree t ->
        if t.value = el then tree
        else if t.value < el then Tree { t with right = insert el t.right}
        else Tree { t with left = insert el t.left}
    | Empty -> Tree {value = el; left = Empty; right = Empty;}

let rec addToTree xs t = 
    match xs with
    | [] -> t
    | [x] -> insert x t
    | l::ls -> insert l (addToTree ls t)


let rec postOrder tree =
    match tree with
    | Empty -> []
    | Tree t -> postOrder t.left @ postOrder t.right @ [t.value]

let rec inOrder tree = 
    match tree with
    | Empty -> []
    | Tree t -> inOrder inOrder t.left @ [t.value] @ inOrder t.right

let rec preOrder tree =
    match tree with
    | Empty -> []
    | Tree t -> t.value::preOrder t.left @ preOrder t.right

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
        if t.value = el then 0
        else if t.value < el then 1 + depth el t.right 
        else 1 + depth el t.left


(* Basic tests*)
// TODO add more tests (look into FsCheck)


let a = Tree {
    value = 1   
    left = Empty
    right = Empty
}
let example_list = [10; 5; 4; 20; 30; 15; 12; 9]
let c = addToTree example_list Empty
let mutable c_equivalent = Empty
c_equivalent <- insert 10 c_equivalent
c_equivalent <- insert 5 c_equivalent
c_equivalent <- insert 4 c_equivalent
c_equivalent <- insert 20 c_equivalent
c_equivalent <- insert 30 c_equivalent
c_equivalent <- insert 15 c_equivalent
c_equivalent <- insert 12 c_equivalent
c_equivalent <- insert 9 c_equivalent
assert c_equivalent = c
let mutable b = insert -2 a
b <- insert 3 b
b <- insert 2 b
b <- insert 4 b
b <- delete 3 b
height b
depth 2 b

printfn $"{b}"
printfn $"{c}"
postOrder c 
