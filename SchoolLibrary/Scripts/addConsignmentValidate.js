$(document).ready(function () {
    
    var count = $('#count');
    var message = $('#message');
    var regexValue = /(^[1-9]{1}$|^[1-9]{1}[0-9]{1}$|^[1-9]{1}[0-9]{1}[0-9]{1}$|^1000)/;
    
    onFocus = count.focus(function () {
        message.text("");
        count.val('');
    });
    
    $('form[action*=Add]').submit(function (eventObject) {
        if (count.val() == '') {
            eventObject.preventDefault();
            message.text(" * Please, enter count!").addClass('field-validation-error');
            onFocus;
        }
        else if (count.val() > 1000) {
            eventObject.preventDefault();
            message.text(" * Your value can not be over 1000. Please, enter another value!").addClass('field-validation-error');
            onFocus;
        }
        else if (!(count.val().match(regexValue))) {
            eventObject.preventDefault();
            message.text(" * Please, enter correct value!").addClass('field-validation-error');
            $(this).focus('');
        }
    });
    
});