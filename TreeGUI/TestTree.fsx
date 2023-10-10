#load "./Tree.fs"
(*
File: TestTree.fs
Authors: Elliot Hagyard and M. Fay Garcia
Purpose: Test the BST implementation and its visualization
*)

module TreeTests =  
    (* Since we're using a .fsx (scripting) file, we have to load the path of the
        module that we want to open before opening it. *)
    
    // Now we can open the package we're trying to test
    open TreeGUI.Tree


    (*Basic BST Tests*)

    // Let's build a basic BST out of a list of values
    let l1 = [10; 5; 4; 20; 100; 15; 6; -5; 120;]

    // addToTree was also tested in the TreeViewer tests
    let t1 = addToTree l1 Empty
    
    // Test height (Expected: 5)
    let h1 = height t1
    printfn $"{h1}"

    // Test depth of 10 (Expected: 4)
    let d1 = depth 10 t1
    printfn $"{d1}"

    // Test depth of 20 (Expected: 5)
    let d2 = depth 20 t1
    printfn $"{d2}"

    // Test depth of 100 (Expected: 4)
    let d3 = depth 100 t1
    printfn $"{d3}"

    // Test depth of 15 (Expected: 3)
    let d4 = depth 15 t1
    printfn $"{d4}"

    // Test depth of 120 (Expected: 0)
    let d5 = depth 120 t1
    printfn $"{d5}"

    // Test preOrder traversal 
    // Expected: [120; -5; 6; 4; 5; 15; 10; 100; 20]
    let pre_t1 = preOrder t1
    printfn "%A" pre_t1;

    // Test inOrder traversal
    // Expected: [-5; 4; 5; 6; 10; 15; 20; 100; 120]
    let in_t1 = inOrder t1
    printfn "%A" in_t1;

    // Test postOrder traversal
    // Expected: [5; 4; 10; 20; 100; 15; 6; -5; 120]
    let post_t1 = postOrder t1
    printfn "%A" post_t1;


    (* F# uses structural equality, so let's test it by comparing
        two trees that we know are equivalent. *)

    let l2 = [10; 5; 4; 20; 30; 15; 12; 9]

    // We can also use this as an opportunity to test our insertion methods.
    let tree_a = addToTree l2 Empty

    // We must declare the variable mutability
    let mutable tree_b = Empty

    // Insertion was also tested in the TreeViewer tests
    tree_b <- insert 10 tree_b
    tree_b <- insert 5 tree_b
    tree_b <- insert 4 tree_b
    tree_b <- insert 20 tree_b
    tree_b <- insert 30 tree_b
    tree_b <- insert 15 tree_b
    tree_b <- insert 12 tree_b
    tree_b <- insert 9 tree_b

    // Now check the equality
    assert (tree_b = tree_a)
    assert (tree_a <> Empty)

    // Test deletion of nodes
    tree_b <- delete 9 tree_b

    // Now we can check the equality again
    assert (tree_b <> tree_a)

    // --------------------------------------- //

    // Now let's create an empty tree
    let mutable b = Empty

    // Let's insert a few more nodes now
    b <- insert 3 b
    b <- insert 2 b
    b <- insert 4 b
    b <- delete 3 b

    // Test the depths of the trees
    assert (depth 10 tree_a = depth 10 tree_b)
    assert (depth 2 b = 2)
    assert (depth -5 b = -1)

        