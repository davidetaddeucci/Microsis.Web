/**
 * Script JavaScript per funzionalità UI del sito Microsis
 */

// Effetto scroll per la navbar
function initNavbarScroll() {
    window.addEventListener('scroll', function () {
        const navbar = document.querySelector('.navbar');
        if (window.scrollY > 50) {
            navbar.classList.add('scrolled');
        } else {
            navbar.classList.remove('scrolled');
        }
    });
}

// Inizializzazione contatori numerici con animazione
function initCounters() {
    document.addEventListener('DOMContentLoaded', function () {
        const counterElements = document.querySelectorAll('.counter-number');

        counterElements.forEach(counter => {
            const target = parseInt(counter.innerText);
            const duration = 2000;
            const step = target / (duration / 16);
            let current = 0;

            const updateCounter = () => {
                current += step;
                if (current < target) {
                    counter.innerText = Math.ceil(current);
                    requestAnimationFrame(updateCounter);
                } else {
                    counter.innerText = target;
                }
            };

            // Osservatore per avviare l'animazione quando l'elemento è visibile
            const observer = new IntersectionObserver((entries) => {
                entries.forEach(entry => {
                    if (entry.isIntersecting) {
                        updateCounter();
                        observer.unobserve(entry.target);
                    }
                });
            }, { threshold: 0.5 });

            observer.observe(counter);
        });
    });
}

// Funzione per scroll fluido per ancore
function initSmoothScroll() {
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();

            const targetId = this.getAttribute('href');
            if (targetId === '#') return;

            const targetElement = document.querySelector(targetId);
            if (targetElement) {
                window.scrollTo({
                    top: targetElement.offsetTop - 100,
                    behavior: 'smooth'
                });
            }
        });
    });
}

// Inizializzazione al caricamento della pagina
document.addEventListener('DOMContentLoaded', function () {
    initNavbarScroll();
    initCounters();
    initSmoothScroll();
});

// Espone le funzioni al contesto globale
window.initNavbarScroll = initNavbarScroll;
window.initCounters = initCounters;
window.initSmoothScroll = initSmoothScroll;