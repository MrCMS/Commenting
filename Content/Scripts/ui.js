(function ($) {
    $(function () {
        $(document).on('click', '[data-link-action]', submitActionForm);
        $(document).on('click', '[data-show-hide-comment]', showHideComment);
        $(document).on('click', '[data-show-reply-to]', showReplyTo);
        $(document).on('click', '[data-hide-reply-to]', hideReplyTo);
        $(document).on('submit', '[data-comments-container] form', checkCaptcha);
    });

    function checkCaptcha(event) {
        $('[data-recaptcha-message-for]').text('');
        var form = $(event.target);
        var recaptcha = form.find('iframe');
        if (recaptcha.length) {
            var id = recaptcha.attr('name').split('_')[0].substring(1);
            if (!grecaptcha.getResponse(id)) {
                event.preventDefault();
                var holder = recaptcha.parents('[data-recaptcha-holder]');
                var message = $('[data-recaptcha-message-for="' + holder.attr('id') + '"]');
                message.text(message.data('error-message'));
            }
        }
    }

    function submitActionForm(event) {
        event.preventDefault();
        var form = $(event.target).parents('form');
        form.submit();
    }

    function showReplyTo(event) {
        var id = $(event.target).data('show-reply-to');
        $('[data-reply-to=' + id + ']').show();
        var recaptchaId = 'reply-recaptcha-' + id;
        var object = $('#' + recaptchaId);
        grecaptcha.render(recaptchaId, {
            sitekey: object.data('sitekey')
        });
        //object.attr('data-recaptcha', captchaId);
    };
    function hideReplyTo(event) {
        event.preventDefault();
        var id = $(event.target).data('hide-reply-to');
        $('[data-reply-to=' + id + ']').hide();
    };
    function showHideComment(event) {
        event.preventDefault();
        var link = $(event.target);
        var post = link.parents('[data-post]').eq(0);
        post.toggleClass('collapsed-post');
        link.toggleClass('glyphicon-plus glyphicon-minus');
    };
})(jQuery);

