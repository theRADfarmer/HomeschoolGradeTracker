// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.body.addEventListener('htmx:afterSwap', function (e) {
    if (e.target.id === 'modal-content' && e.detail.xhr.status === 200) {
        if (e.detail.xhr.response.includes('success-marker')) {
            setTimeout(() => {
                window.dispatchEvent(new CustomEvent('close-modal'));

                const subjectTable = document.querySelector('#subject-table');
                if (subjectTable) {
                    htmx.trigger(subjectTable, 'refresh');
                }
            }, 0);
        }
    }
});

// HTMX event listeners for debugging
/*
document.body.addEventListener("htmx:afterRequest", function (evt) {
    console.log("HTMX request finished", evt.detail);
});

document.body.addEventListener("htmx:swapError", function (evt) {
    console.error("HTMX swap error", evt);
});
*/