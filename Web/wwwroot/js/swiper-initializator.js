
window.initializeSwiper = (id) => {
	const swiper = new Swiper(`.swiper-${id}`, {
		direction: 'horizontal',
		slidesPerView: "auto",
		spaceBetween: 5,
		loop: false,
		grabCursor: true,
		centeredSlides: false,
		autoplay: {
			delay: 5000,
		},

		pagination: {
			el: `.swiper-pagination-${id}`,
		},

		navigation: {
			nextEl: `.swiper-button-next-${id}`,
			prevEl: `.swiper-button-prev-${id}`,
		},
	});
}