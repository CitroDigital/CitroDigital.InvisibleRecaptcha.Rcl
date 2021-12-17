const recaptchaDataAttributeKey = 'data-recaptcha-validation-token'; 
$(document).on('click',
    `input[type="submit"][data-validate-recaptcha]`,
    function(e) {
        e.preventDefault();
        e.stopPropagation();

        const formElement = this.closest('form');
        const recaptchaElement = formElement.querySelector(`input[${recaptchaDataAttributeKey}]`);

        const action = recaptchaElement.getAttribute('data-action');
        const siteKey = recaptchaElement.getAttribute('data-site-key');

        grecaptcha.ready(() =>
            grecaptcha.execute(siteKey, { action: action })
            .then((token) => {
                recaptchaElement.value = token;
                const submitEvent = new SubmitEvent("submit");
                formElement.dispatchEvent(submitEvent);

            })
            .catch(error => console.error(error))
        );
    });