// //const $next = document.querySelector('.next');
// //const $prev = document.querySelector('.prev');

// //function nextSlide() {
//   //const items = document.querySelectorAll('.item');
//   //document.querySelector('.slide').appendChild(items[0]);
// //}

// //function prevSlide() {
//   //const items = document.querySelectorAll('.item');
//   //document.querySelector('.slide').prepend(items[items.length - 1]);
// //}

// // Add event listeners for next and previous buttons
// //$next.addEventListener('click', nextSlide);
// //$prev.addEventListener('click', prevSlide);

// // Automatically move to the next slide every 3 seconds
// //let slideInterval = setInterval(nextSlide, 3000);

// // Optionally, you can pause the automatic slide when hovering over the slide
// //document.querySelector('.slide').addEventListener('mouseenter', () => {
//   //clearInterval(slideInterval);
// //});

// //document.querySelector('.slide').addEventListener('mouseleave', () => {
//   //slideInterval = setInterval(nextSlide, 3000);
// //});

// const $next = document.querySelector('.next');
// const $prev = document.querySelector('.prev');

// // Function to go to the next slide
// const goToNextSlide = () => {
//   const items = document.querySelectorAll('.item');
//   document.querySelector('.slide').appendChild(items[0]);
// };

// // Function to go to the previous slide
// const goToPrevSlide = () => {
//   const items = document.querySelectorAll('.item');
//   document.querySelector('.slide').prepend(items[items.length - 1]);
// };

// // Add event listeners for manual navigation
// $next.addEventListener('click', goToNextSlide);
// $prev.addEventListener('click', goToPrevSlide);

// // Automatically go to the next slide every 3 seconds
// let autoSlideInterval = setInterval(goToNextSlide, 3000);

// // Pause automatic sliding when the user clicks on "Next" or "Previous"
// const pauseAutoSlide = () => {
//   clearInterval(autoSlideInterval);
//   // Restart the automatic sliding after a delay of 5 seconds
//   autoSlideInterval = setInterval(goToNextSlide, 5000);
// };

// $next.addEventListener('click', pauseAutoSlide);
// $prev.addEventListener('click', pauseAutoSlide);

