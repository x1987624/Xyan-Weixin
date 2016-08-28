﻿var senparc = {};
var maxSubMenuCount = 5;
var menuState;
senparc.menu = {
    token: '',
    init: function () {
        menuState = $('#menuState');

        $('#buttonDetails').hide();

        $("#buttonDetails_type").change(senparc.menu.typeChanged);

        $(':input[id^=menu_button]').click(function () {
            $('#buttonDetails').show();
            var idPrefix = $(this).attr('data-root')
                            ? ('menu_button' + $(this).attr('data-root'))
                            : ('menu_button' + $(this).attr('data-j') + '_sub_button' + $(this).attr('data-i'));

            var keyId = idPrefix + "_key";
            var nameId = idPrefix + "_name";
            var typeId = idPrefix + "_type";
            var urlId = idPrefix + "_url";

            var txtDetailsKey = $('#buttonDetails_key');
            var txtDetailsName = $('#buttonDetails_name');
            var ddlDetailsType = $('#buttonDetails_type');
            var txtDetailsUrl = $('#buttonDetails_url');

            var hiddenButtonKey = $('#' + keyId);
            var hiddenButtonType = $('#' + typeId);
            var hiddenButtonUrl = $('#' + urlId);

            txtDetailsKey.val(hiddenButtonKey.val());
            txtDetailsName.val($('#' + nameId).val());
            ddlDetailsType.val(hiddenButtonType.val());
            txtDetailsUrl.val(hiddenButtonUrl.val());

            senparc.menu.typeChanged();

            txtDetailsKey.unbind('blur').blur(function () {
                hiddenButtonKey.val($(this).val());
            });
            ddlDetailsType.unbind('blur').blur(function () {
                hiddenButtonType.val($(this).val());
            });
            txtDetailsUrl.unbind('blur').blur(function () {
                hiddenButtonUrl.val($(this).val());
            });
        });


        $('#menuLogin_SubmitOldToken').click(function () {
            senparc.menu.setToken($('#menuLogin_OldToken').val());
        });

        $('#btnGetMenu').click(function () {
            menuState.html('获取菜单中...');
            $.getJSON('/WeixinMenu/GetMenu?t=' + Math.random(), { token: senparc.menu.token }, function (json) {
                if (json.menu) {
                    $(':input[id^=menu_button]:not([id$=_type])').val('');
                    $('#buttonDetails:input').val('');

                    var buttons = json.menu.button;
                    //此处i与j和页面中反转
                    for (var i = 0; i < buttons.length; i++) {
                        var button = buttons[i];
                        $('#menu_button' + i + '_name').val(button.name);
                        $('#menu_button' + i + '_key').val(button.key);
                        $('#menu_button' + i + '_type').val(button.type || 'click');
                        $('#menu_button' + i + '_url').val(button.url);

                        if (button.sub_button && button.sub_button.length > 0) {
                            //二级菜单
                            for (var j = 0; j < button.sub_button.length; j++) {
                                var subButton = button.sub_button[j];
                                var idPrefix = '#menu_button' + i + '_sub_button' + j;
                                $(idPrefix + "_name").val(subButton.name);
                                $(idPrefix + "_type").val(subButton.type || 'click');
                                $(idPrefix + "_key").val(subButton.key);
                                $(idPrefix + "_url").val(subButton.url);
                            }
                        } else {
                            //底部菜单
                            //...
                        }
                    }

                    //显示JSON
                    $('#txtReveiceJSON').html(JSON.stringify(json));

                    menuState.html('菜单获取已完成');
                } else {
                    menuState.html(json.error || '执行过程有错误，请检查！');
                }
            });
        });

        $('#btnDeleteMenu').click(function () {
            if (!confirm('确定要删除菜单吗？此操作无法撤销！')) {
                return;
            }

            menuState.html('删除菜单中...');
            $.getJSON('/WeixinMenu/DeleteMenu?t=' + Math.random(), { token: senparc.menu.token }, function (json) {
                if (json.Success) {
                    menuState.html('删除成功，如果是误删，并且界面上有最新的菜单状态，可以立即点击【更新到服务器】按钮。');
                } else {
                    menuState.html(json.Message);
                }
            });
        });

        $('#submitMenu').click(function () {
            if (!confirm('确定要提交吗？此操作无法撤销！')) {
                return;
            }

            menuState.html('上传中...');

            $('#form_Menu').ajaxSubmit({
                dataType: 'json',
                success: function (json) {
                    if (json.Successed) {
                        menuState.html('上传成功');
                    } else {
                        menuState.html(json.Message);
                    }
                }
            });
        });
    },
    typeChanged: function () {
        var val = $('#buttonDetails_type').val();
        if (val.toUpperCase() == 'VIEW') {
            $('#buttonDetails_key_area').slideUp(100);
            $('#buttonDetails_url_area').slideDown(100);
        } else {
            $('#buttonDetails_key_area').slideDown(100);
            $('#buttonDetails_url_area').slideUp(100);
        }
    },
    setToken: function (token) {
        senparc.menu.token = token;
        $('#tokenStr').val(token);
        $('#menuEditor').show();
        $('#menuLogin').hide();
    }
};