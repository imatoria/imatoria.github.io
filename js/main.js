/**
* Template Name: Personal - v4.7.0
* Template URL: https://bootstrapmade.com/personal-free-resume-bootstrap-template/
* Author: BootstrapMade.com
* License: https://bootstrapmade.com/license/
*/


"use strict";

/**
 * Easy selector helper function
 */
const select = (el, all = false) => {
    el = el.trim()
    if (all) {
        return [...document.querySelectorAll(el)]
    } else {
        return document.querySelector(el)
    }
}

/**
 * Easy event listener function
 */
const on = (type, el, listener, all = false) => {
    let selectEl = select(el, all)
    if (selectEl) {
        if (all) {
            selectEl.forEach(e => e.addEventListener(type, listener))
        } else {
            selectEl.addEventListener(type, listener)
        }
    }
}

/**
 * Scrolls to an element with header offset
 */
const scrollto = (el) => {
    window.scrollTo({
        top: 0,
        behavior: 'smooth'
    })
}

function onAssemblyLoad() {
    /**
     * Mobile nav toggle
     */
    on('click', '.mobile-nav-toggle', function (e) {
        select('#navbar').classList.toggle('navbar-mobile')
        this.classList.toggle('bi-list')
        this.classList.toggle('bi-x')
    });

    // Github Pages shows 404 page when reloading.
    // Fix =>
    //      Force page to navigate to homepage with returnUrl.
    //      Click the corresponding element for returnUrl.
    if (this.location.hostname !== "localhost") {
        let pathname = this.location.pathname.substring(1);
        if (pathname != "") {
            let allowedPaths = ["about", "resume", "projects", "portfolio", "portfolio/tic-tac-toe", "portfolio/todo", "contact"];
            if (allowedPaths.includes(pathname) === false) {
                return;
            }
            window.location.href = "/?returnUrl=" + encodeURI(pathname);
        } else if (this.location.search != "") {
            let href = this.location.search.split("=")[1];
            select("[href='" + href + "']").click();
        }
    }
    
}
function onPageLoad(href) {
    let _href = href.replace(/\//g, "_");
    let section = select("#" + _href)
    if (section == null) {
        return;
    }

    let navbar = select('#navbar')
    let header = select('#header')
    let sections = select('section', true)
    let navlinks = select('#navbar .nav-link', true)

    navlinks.forEach((item) => {
        if (item.getAttribute("href") == href) {
            item.classList.add('active')
        } else {
            item.classList.remove('active')
        }
    })

    if (navbar.classList.contains('navbar-mobile')) {
        navbar.classList.remove('navbar-mobile')
        let navbarToggle = select('.mobile-nav-toggle')
        navbarToggle.classList.toggle('bi-list')
        navbarToggle.classList.toggle('bi-x')
    }
    
    if (href == 'home') {
        header.classList.remove('header-top')
        sections.forEach((item) => {
            item.classList.remove('section-show')
        })
        return;
    }

    if (!header.classList.contains('header-top')) {
        header.classList.add('header-top')
        setTimeout(function () {
            sections.forEach((item) => {
                item.classList.remove('section-show')
            })
            section.classList.add('section-show')

        }, 350);
    } else {
        sections.forEach((item) => {
            item.classList.remove('section-show')
        })
        section.classList.add('section-show')
    }

    scrollto(_href)
}

/**
 * Skills animation
 */
let skilsContent = select('.skills-content');
if (skilsContent) {
    new Waypoint({
        element: skilsContent,
        offset: '80%',
        handler: function (direction) {
            let progress = select('.progress .progress-bar', true);
            progress.forEach((el) => {
                el.style.width = el.getAttribute('aria-valuenow') + '%'
            });
        }
    })
}

/**
 * Testimonials slider
 */
new Swiper('.testimonials-slider', {
    speed: 600,
    loop: true,
    autoplay: {
        delay: 5000,
        disableOnInteraction: false
    },
    slidesPerView: 'auto',
    pagination: {
        el: '.swiper-pagination',
        type: 'bullets',
        clickable: true
    },
    breakpoints: {
        320: {
            slidesPerView: 1,
            spaceBetween: 20
        },

        1200: {
            slidesPerView: 3,
            spaceBetween: 20
        }
    }
});

/**
 * Porfolio isotope and filter
 */
window.addEventListener('load', () => {
    let portfolioContainer = select('.portfolio-container');
    if (portfolioContainer) {
        let portfolioIsotope = new Isotope(portfolioContainer, {
            itemSelector: '.portfolio-item',
            layoutMode: 'fitRows'
        });

        let portfolioFilters = select('#portfolio-flters li', true);

        on('click', '#portfolio-flters li', function (e) {
            e.preventDefault();
            portfolioFilters.forEach(function (el) {
                el.classList.remove('filter-active');
            });
            this.classList.add('filter-active');

            portfolioIsotope.arrange({
                filter: this.getAttribute('data-filter')
            });
        }, true);
    }

});

/**
 * Initiate portfolio lightbox 
 */
const portfolioLightbox = GLightbox({
    selector: '.portfolio-lightbox'
});

/**
 * Initiate portfolio details lightbox 
 */
const portfolioDetailsLightbox = GLightbox({
    selector: '.portfolio-details-lightbox',
    width: '90%',
    height: '90vh'
});

/**
 * Portfolio details slider
 */
new Swiper('.portfolio-details-slider', {
    speed: 400,
    loop: true,
    autoplay: {
        delay: 5000,
        disableOnInteraction: false
    },
    pagination: {
        el: '.swiper-pagination',
        type: 'bullets',
        clickable: true
    }
});
