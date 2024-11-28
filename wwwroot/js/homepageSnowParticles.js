particlesJS("snow-particles", {
    particles: {
        number: { 
            value: 150 
        },
        color: { 
            value: "#ffffff" 
        },
        shape: { 
            type: "circle",
            // stroke: {
            //     width: 1,
            //     color: "#808080" 
            // }
        },
        opacity: { 
            value: 0.8, 
            random: true 
        },
        size: { 
            value: 4, 
            random: true 
        },
        move: {
            enable: true,
            speed: 1, 
            direction: "bottom", 
            random: true, 
            straight: false, 
            out_mode: "out" 
        },
        line_linked: {
            enable: false 
        }
    },
    interactivity: {
        detect_on: "canvas",
        events: {
            onhover: { 
                enable: false 
            },
            onclick: { 
                enable: false 
            },
            resize: true 
        }
    },
    retina_detect: true 
});