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

// Gestione click fuori dal dropdown lingua
window.setupLanguageDropdownClick = function (dotnetHelper) {
    window.addEventListener('click', function (event) {
        // Se il click non è sul globo né sul dropdown
        const langSelector = document.querySelector('.nav-globe-button');
        const dropdown = document.querySelector('.language-dropdown');
        
        if (langSelector && dropdown) {
            if (!langSelector.contains(event.target) && !dropdown.contains(event.target)) {
                dotnetHelper.invokeMethodAsync('CloseDropdown');
            }
        }
    });
};

// Gestione click fuori dal dropdown lingua
window.addClickOutsideListener = function (dotnetHelper) {
    window.clickOutsideHandler = function (event) {
        const languageSelector = document.querySelector('.language-selector');
        if (languageSelector && !languageSelector.contains(event.target)) {
            dotnetHelper.invokeMethodAsync('CloseDropdown');
        }
    };
    
    document.addEventListener('click', window.clickOutsideHandler);
};

window.removeClickOutsideListener = function () {
    if (window.clickOutsideHandler) {
        document.removeEventListener('click', window.clickOutsideHandler);
    }
};

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