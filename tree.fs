type Tree = { 
    Value : int
    left : Root
    right : Root
}
and Root = 
    | Tree of Tree
    | Empty


let a = Tree {
    Value = 1   
    left = Empty
    right = Empty
}

let rec insert tree el = 
    match tree with
    | Tree t -> 
        if t.Value = el then tree 
        else if t.Value < el then Tree { t with right = insert t.right el}
        else Tree { t with left = insert t.left el}
    | Empty -> Tree {Value=el; left=Empty; right=Empty}


printfn $"aaa {a} aaa"