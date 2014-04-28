function getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}
function getRandomArray(size, min, max) {
    var array = [];
    for (i = 0; i < size; i++) {
        array.push(getRandomInt(min, max));
    }
    return array;
}
function printDoubleArray(array) {
    console.log(array);
}