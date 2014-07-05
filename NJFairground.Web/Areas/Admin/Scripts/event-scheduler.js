/// <reference path="../../../Scripts/jquery-2.1.0-vsdoc.js" />
/// <reference path="../../../Scripts/jquery-2.1.1.min.js" />
/// <reference path="../../../Scripts/common-script.js" />
/// <reference path="common-admin-ui-script.js" />
/// <reference path="json3.js" />





(function ($, win) {
    $(document).ready(function () {
        try {
            GetEventData();
        } catch (ex) {
            console.log(ex);
        }
    });

    var configureCalender = function (eventData) {
        try {
            var currentDate = new Date();
            $('#calendar').fullCalendar({
                header: {
                    left: 'prev, next today',
                    center: 'title',
                    right: 'month, agendaWeek, agendaDay'
                },
                defaultDate: $.formatDateTime('yy-mm-dd', new Date()),
                defaultView: 'agendaWeek',
                editable: true,
                eventResize: function (event, delta, revertFunc, jsEvent, ui, view) {
                    try {
                        ConfirmUI('Confirm move?', function () {
                            updateEvents({
                                id: event.id,
                                pageid: event.pageid,
                                title: event.title,
                                start: event.start,
                                end: event.end,
                                statusid: event.statusid,
                                description: event.description
                            });
                        }, function () {
                            revertFunc();
                        });
                    } catch (ex) {
                        console.log(ex);
                    }
                },
                eventDrop: function (event, delta, revertFunc, jsEvent, ui, view) {
                    try {
                        ConfirmUI("Confirm change appointment length?", function () {
                            updateEvents({
                                id: event.id,
                                pageid: event.pageid,
                                title: event.title,
                                start: event.start,
                                end: event.end,
                                statusid: event.statusid,
                                description: event.description
                            });
                        }, function () {
                            revertFunc();
                        });
                    } catch (ex) {
                        console.log(ex);
                    }
                },
                //events: eventData
                events: '{0}/Admin/EventScheduler/GetEventData'.format(virtualDirectory)
            });
        } catch (ex) {
            console.log(ex);
        }
    }

    var GetEventData = function () {
        try {
            $.ajax({
                type: 'GET',
                url: '{0}/Admin/EventScheduler/GetEventData'.format(virtualDirectory),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (result) {
                    if (result.Status == 'success') {
                        configureCalender(result.Data);
                    }
                },
                error: function (status, error) {
                    alert(status);
                }
            });
        } catch (ex) {
            console.log(ex);
        }
    };

    var updateEvents = function (eventData) {
        try {
            $.ajax({
                type: 'POST',
                url: '{0}/Admin/EventScheduler/SetEventData'.format(virtualDirectory),
                data: JSON.stringify(eventData),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (result) {
                    if (result) {

                    }
                },
                error: function (status, error) {
                    alert(status);
                }
            });
        } catch (ex) {
            console.log(ex);
        }
    };

    this.frmAddEvent_OnBeginHandler = function () {
        try {

        } catch (ex) {
            console.log(ex);
        }
    };

    this.frmAddEvent_OnSuccess = function () {
        try {

        } catch (ex) {
            console.log(ex);
        }
    };

    this.frmAddEvent_OnComplete = function () {
        try {

        } catch (ex) {
            console.log(ex);
        }
    };

    this.openAddEventWindow = function () {
        try {
            $.ajax({
                type: 'GET',
                url: '{0}/Admin/EventScheduler/GetAddEventDialog'.format(virtualDirectory),
                success: function (result) {
                    if (result) {
                        modal.open({
                            content: result,
                            width:'735px' ,
                            openCallBack: function (element) {
                                var $self = $(element)
                                $self.find('#start').datetimepicker();
                                $self.find('#end').datetimepicker();

                                initHtmlEditor();
                            }
                        });
                    }
                },
                error: function (status, error) {
                    alert(status);
                }
            });
        } catch (ex) {
            console.log(ex);
        }
    }

    var initHtmlEditor = function () {
        try {
            tinyMCE.init({
                // General options
                mode: "exact",
                elements: "description",
                theme: "advanced",
                width: "742",
                height: "274",
                spellchecker_languages: "+English=en,Swedish=sv",

                plugins: "autolink,lists,spellchecker,pagebreak,style,layer,table,save,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,searchreplace,print,contextmenu,paste,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template",

                // Theme options
                theme_advanced_buttons1: "bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,formatselect,fontselect,fontsizeselect",
                theme_advanced_buttons2: "cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,cleanup,|,insertdate,inserttime,preview,|,forecolor,backcolor,|,sub,sup,|,charmap,emotions,spellchecker",
                theme_advanced_toolbar_location: "top",
                theme_advanced_toolbar_align: "left",
                //theme_advanced_statusbar_location: "bottom",
                theme_advanced_path: false,
                theme_advanced_resize_horizontal: false,
                //encoding : "xml",
                // Skin options
                skin: "o2k7"
            });
        } catch (ex) {
            console.log(ex);
        }
    }

}(jQuery, window));