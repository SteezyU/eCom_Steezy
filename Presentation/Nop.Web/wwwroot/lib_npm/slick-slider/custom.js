$(document).ready(function(){
  $('.trending-slider').slick({
    dots: false,
    infinite: true,
    speed: 300,
    slidesToShow: 5,
    slidesToScroll: 1,
    prevArrow: '<div class="slide-arrow prev"><img src="themes/defaultclean/content/images/steezy/icons/prev.png"></div>',
    nextArrow: '<div class="slide-arrow next"><img src="themes/defaultclean/content/images/steezy/icons/next.png"></div>',
    responsive: [
      {
        breakpoint: 1024,
        settings: {
          slidesToShow: 3,
          slidesToScroll: 3,
          infinite: true,
        }
      },
      {
        breakpoint: 600,
        settings: {
          slidesToShow: 2,
          slidesToScroll: 2,
          dots: true,
          arrows: false
        }
      },
      {
        breakpoint: 480,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1,
          dots: true,
          arrows: false
        }
      }
      // You can unslick at a given breakpoint now by adding:
      // settings: "unslick"
      // instead of a settings object
    ]
  });


  $(".filter-btn").click(function(){
    $("body").prepend("<div class='category-overlay'></div>")
    $(".side-2").animate({left: 0}, 100)
  })
  $(document).on("click", ".category-overlay", function(){
    $(this).remove()
    $(".side-2").animate({left: -250}, 100)
  })
})


