(*
File: Tree.fs
Authors: Elliot Hagyard and M. Fay Garcia
Purpose: Implement a Binary Search Tree for Integers
*)
namespace TreeGUI

module Tree = 
    (*While this definition works for a tree of any type,
        we are focusing on Binary Search Trees containing integers.*)
    type Root<'a> = 
        | Tree of {|value : 'a; left : 'a Root; right : 'a Root|}
        | Empty

    // Get the height of the tree
    let rec height tree = 
        match tree with
        | Empty -> 0
        | Tree t -> 
            match (t.left, t.right) with 
            | (Empty, Empty) -> 0
            | (_, _) -> 1 + max (height t.left) (height t.right)
    
    // Get the depth of the node in the tree with the given value
    let depth el tree = 
        // Here we declare an inner function to track the depth
        let rec depthR el tree level =
            match tree with
            | Empty -> -1
            | Tree t -> 
                if t.value = el then level
                else if t.value < el then depthR el t.right (level + 1)
                else depthR el t.left (level + 1)
        depthR el tree 0
    
    // Delete a node with the given value
    let rec delete el tree = 
        match tree with
        | Empty -> Empty
        | Tree t -> 
            if t.value = el then 
                match (t.left, t.right) with
                | (Empty, _) -> t.right
                | (_, Empty) -> t.left
                | (Tree l, Tree r) -> Tree {|
                    value = l.value; 
                    right = Tree r;
                    left = delete l.value t.left
                |}
            else if t.value < el then Tree {| t with right = delete el t.right |}
            else Tree {| t with left = delete el t.left |}
    
    // Insert a node with the given value
    let rec insert el tree = 
        match tree with
        | Tree t ->
            if t.value = el then tree
            else if t.value < el then Tree {| t with right = insert el t.right |}
            else Tree {| t with left = insert el t.left |}
        | Empty -> Tree{|value = el; left = Empty; right = Empty;|}
    
    // Add a list of values to the tree
    let rec addToTree xs t = 
        match xs with
        | [] -> t
        | [x] -> insert x t
        | l::ls -> insert l (addToTree ls t)
    
    // TRAVERSALS: //

    // Return the preOrder traversal of the tree
    let rec preOrder tree =
        match tree with
        | Empty -> []
        | Tree t -> t.value::preOrder t.left @ preOrder t.right
    
    // Return the inOrder traversal of the tree
    let rec inOrder tree = 
        match tree with
        | Empty -> []
        | Tree t -> inOrder t.left @ [t.value] @ inOrder t.right

    // Return the postOrder traversal of the tree
    let rec postOrder tree =
        match tree with
        | Empty -> []
        | Tree t -> postOrder t.left @ postOrder t.right @ [t.value]