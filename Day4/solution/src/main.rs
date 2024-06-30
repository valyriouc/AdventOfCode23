
fn main() {
    // Reading in the content 

    let input = std::fs::read_to_string("../input.txt")
        .expect("Here we go");

    println!("{}", input);
    
    return;
}
