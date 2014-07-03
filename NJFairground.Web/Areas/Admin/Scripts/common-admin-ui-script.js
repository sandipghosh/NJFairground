
(function ($, win) {

    $.ajaxSetup({
        beforeSend: function (jqXHR, settings) {
            if (!settings.url.contains('Keepalive')) {
                loadingCounter += 1;
                $(document).css('cursor', 'wait !important');
                try { $.blockUI({ message: $("#dataloading") }); } catch (ex) { }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert('error');
        },
        complete: function (jqXHR, textStatus) {
            if (loadingCounter > 1)
            { loadingCounter -= 1 }
            else {
                loadingCounter = 0;
                try { $.unblockUI(); } catch (ex) { }
            }
            $(document).css('cursor', 'default !important');
        }
    });

    /*Implementing function to overload general alert function*/
    win.alert = function (msg, okhandle, height, width, okCaption, title) {
        try {
            msg = msg.toString();
            $(function () {
                if (okCaption != undefined || okCaption != null)
                    $('#dialog-message').attr('title', title);
                else
                    $('#dialog-message').attr('title', 'Message');

                $("#dialog-message p").html('').addClass('alert');

                if (msg.indexOf('#') > 0) {
                    $.each(msg.split('#'), function (index, value) {
                        if (value != '') {
                            $("#dialog-message p.alert").append('<li>{0}</li>'.format(value));
                        }
                    });
                    $("#dialog-message p.alert").wrapInner('<ul/>');
                }
                else {
                    $("#dialog-message p.alert").html(msg);
                }

                $("#dialog:ui-dialog").dialog("destroy");

                $("#dialog-message").dialog({
                    modal: true,
                    position: ['center', 'middle'],
                    height: (height != undefined || height != null) ? height : 'auto',
                    width: (width != undefined || width != null) ? width : 'auto',
                    autoResize: true,
                    resizable: false,
                    open: function (event, ui) {
                        $("#dialog-message").closest('div.ui-dialog').MakeCenterScreen();
                    },
                    buttons: [
                        {
                            text: (okCaption != undefined || okCaption != null) ? okCaption : "OK",
                            click: function () {
                                $(this).dialog("close");
                                if (okhandle != undefined || okhandle != null) {
                                    okhandle();
                                }
                            }
                        }
                    ]
                });
            });
        } catch (ex) {
            if (window.console)
                window.console.log(ex);
        }
    };

    /*Implementing function to overload general confirm function*/
    this.ConfirmUI = function (msg, yeshandle, nohandle, height, width, okCaption, cancelCaption) {
        try {
            $("#dialog-message p").html('');
            $("#dialog-message p").addClass('confirmation');

            if (msg.indexOf('#') > 0) {
                $.each(msg.split('#'), function (index, value) {
                    if (value != '') {
                        $("#dialog-message p.confirmation").append('<li>{0}</li>'.format(value));
                    }
                });
                $("#dialog-message p.confirmation").wrapInner('<ul/>');
            }
            else {
                $("#dialog-message p.confirmation").html(msg);
            }

            $("#dialog:ui-dialog").dialog("destroy");

            $("#dialog-message").dialog({
                modal: true,
                position: ['center', 'center'],
                height: (height != undefined || height != null) ? height : 'auto',
                width: (width != undefined || width != null) ? width : 'auto',
                autoResize: true,
                resizable: false,
                buttons: [
                    {
                        text: (okCaption != undefined || okCaption != null) ? okCaption : "YES",
                        click: function () {
                            $(this).dialog("close");
                            if (yeshandle != undefined || yeshandle != null) {
                                yeshandle();
                            }
                        }
                    },
                    {
                        text: (cancelCaption != undefined || cancelCaption != null) ? cancelCaption : "NO",
                        click: function () {
                            $(this).dialog("close");
                            if (nohandle != undefined || nohandle != null) {
                                nohandle();
                            }
                        }
                    }
                ]
            });
        } catch (ex) {
            if (window.console)
                window.console.log(ex);
        }
    };

}(jQuery, window));

$(window).resize(function () {
    modal.reposition();
});

var modal = (function () {
    var
    method = {},
    $overlay,
    $modal,
    $content,
    $close;

    // Center the modal in the viewport
    method.center = function () {
        var top, left;

        top = Math.max($(window).height() - $modal.outerHeight(), 0) / 2;
        left = Math.max($(window).width() - $modal.outerWidth(), 0) / 2;

        $modal.css({
            top: top + $(window).scrollTop(),
            left: left + $(window).scrollLeft()
        });
    };

    // Open the modal
    method.open = function (settings) {
        $content.empty().append(settings.content);

        $modal.css({
            width: settings.width || 'auto',
            height: settings.height || 'auto'
        });

        method.center();
        //$(window).trigger('resize');
        $(window).bind('resize.modal', method.center);

        if (typeof settings.openCallBack !== 'undefined')
            settings.openCallBack($modal);

        $modal.show();
        $overlay.show();
    };

    // Close the modal
    method.close = function () {
        $modal.hide();
        $overlay.hide();
        $content.empty();
        $(window).unbind('resize.modal');
    };

    method.reposition = function () {
        var topPos = ($(window.top).height() - $modal.outerHeight()) / 2;

        $modal.css({
            left: ($(window).width() - $modal.outerWidth()) / 2,
            //top: topPos > 0 ? topPos : 0
            top: 30
        });
    }

    // Generate the HTML and add it to the document
    $overlay = $('<div class="model-overlay"></div>');
    $modal = $('<div class="modal-container"></div>');
    $content = $('<div class="model-content"></div>');
    $close = $('<a class="modal-close" href="#">close</a>');

    $modal.hide();
    $overlay.hide();
    $modal.append($content, $close);

    $(document).ready(function () {
        $('body').append($overlay, $modal);
    });

    $close.click(function (e) {
        e.preventDefault();
        method.close();
    });

    return method;
}());
