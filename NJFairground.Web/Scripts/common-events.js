(function ($, win) {
    $(function () {
        $(document).ready(function () {
            var $self = $(this).find("div[data-role*='page']");
            PageRepostioning($self);
        });

        //Device portrait/landscape orientation event
        $(win).on("orientationchange", function (event) {
            try {
                var $self = $(this).find("div[data-role*='page']");
                PageRepostioning($self);
            } catch (ex) {
                console.log(ex);
            }
        });

        //Triggered on the "toPage" after the transition animation has completed.
        $("div[data-role*='page']").live('pageshow', function (event, ui) {
            try {
                var $self = $(this);
                PageRepostioning($self);
            } catch (ex) {
                console.log(ex);
            }
        });


        //Triggered when the page has been created in the DOM (via ajax or other) and 
        //after all widgets have had an opportunity to enhance the contained markup.
        $("div[data-role*='page']").live('pagecreate', function (event, ui) {
            try {

            } catch (ex) {
                console.log(ex);
            }
        });

        //Triggered on the "toPage" we are transitioning to, 
        //before the actual transition animation is kicked off.
        $("div[data-role*='page']").live('pagebeforeshow', function (event, ui) {
            try {
                //var $self = $(this);
                //PageRepostioning($self);
            } catch (ex) {
                console.log(ex);
            }
        });

        //Triggered when a swipe event occurs moving in the left direction.
        $('#body-main').live('swipeleft', function (event) {
            try {
                win.history.forward();
                event.preventDefault();
                refreshPage();
            } catch (ex) {
                console.log(ex);
            }
        });

        $('.ui-icon-back').live('tap', function (event) {
            try {
                win.history.back();
                event.preventDefault();
                refreshPage();
            } catch (ex) {
                console.log(ex);
            }
        });

        //Triggered when a swipe event occurs moving in the right direction.
        $('#body-main').live('swiperight', function (event) {
            try {
                win.history.back();
                event.preventDefault();
                refreshPage();
            } catch (ex) {
                console.log(ex);
            }
        });


        $('.fun-block,.social-block,.info-block,.block').live("tap", function () {
            try {
                var url = $(this).find('a').attr('href');
                window.location.href = url;
            } catch (ex) {
                console.log(ex);
            }
        });

        $('.header-back,.back').live('tap', function (event) {
            try {
                win.history.back();
                event.preventDefault();
            } catch (ex) {
                console.log(ex);
            }
        });
    });

    //General function implementation to resize the main body.
    this.PageRepostioning = function ($self) {
        try {
            var docHeight = $(win).height();

            var headerHeight = parseInt($self.find('#header-main').css('height').replace('px', ''));
            var footerHeight = parseInt($self.find('#footer-main').css('height').replace('px', ''));

            var bodyHeight = (docHeight - (headerHeight + footerHeight));
            $self.find('#body-main').css({ 'height': bodyHeight + 'px', 'margin-top': headerHeight + 'px' });

        } catch (ex) {
            console.log(ex);
        }
    };

    this.refreshPage = function () {
        $.mobile.changePage(window.location.href,
          {
              allowSamePageTransition: true,
              transition: 'none',
              showLoadMsg: false,
              reloadPage: true
          });
    }
}(jQuery, window));