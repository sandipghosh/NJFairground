(function ($, win) {
    this.InitiateEditor = function (selector, removeEditorFirst) {
        try {
            if (removeEditorFirst)
                RemoveEditor(selector);

            tinymce.init({
                selector: selector,
                theme: "modern",
                menubar: false,
                height: 300,
                statusbar: false,
                plugins: [
                     "advlist autolink link image lists charmap print preview hr anchor pagebreak spellchecker",
                     "searchreplace wordcount visualblocks visualchars code fullscreen insertdatetime media nonbreaking",
                     "save table contextmenu directionality emoticons template paste textcolor code"
                ],
                toolbar: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image | preview media fullpage | forecolor backcolor emoticons code",
            });
        } catch (ex) { }
    };

    this.RemoveEditor = function (selector) {
        try {
            $.each(selector.split(','), function (index, element) {
                element = element.replace('#', '').trim();
                tinymce.EditorManager.execCommand('mceRemoveEditor', true, element);
            });
        } catch (ex) { }
    };

    this.AjaxFileUpload = function (callback) {
        window.addEventListener("submit", function (e) {
            var form = e.target;
            if (form.getAttribute("enctype") === "multipart/form-data") {
                if (form.dataset.ajax) {
                    e.preventDefault();
                    e.stopImmediatePropagation();
                    tinyMCE.triggerSave(true, true);

                    var xhr = new XMLHttpRequest();
                    xhr.open(form.method, form.action);
                    xhr.onreadystatechange = function () {
                        if (xhr.readyState == 4 && xhr.status == 200) {
                            if (typeof callback != 'undefined')
                                callback(xhr.responseText)
                        }
                    };
                    xhr.send(new FormData(form));
                }
            }
        }, true);
    };

    $.ajaxSetup({
        beforeSend: function (jqXHR, settings) {
            FormBeginSend();
        },
        complete: function (jqXHR, textStatus) {
            FormCompleteSend();
        }
    });

    this.FormBeginSend = function () {
        try {
            if (!$('.blocker').is(':visible')) {
                loadingCounter += 1;
                $(document).css('cursor', 'wait !important');
                $('.blocker').show();
            }
        } catch (ex) { }
    };

    this.FormCompleteSend = function () {
        try {
            if (loadingCounter > 1)
            { loadingCounter -= 1 }
            else {
                if ($('.blocker').is(':visible')) {
                    loadingCounter = 0;
                    $('.blocker').hide();
                    $(document).css('cursor', 'default !important');
                }
            }
        } catch (ex) { }
    };
}(jQuery, window));