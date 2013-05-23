
jQuery.validator.addMethod("async", function (value, element, param) {
    if (this.optional(element)) {
        return "dependency-mismatch";
    }

    var previous = this.previousValue(element);
    if (!this.settings.messages[element.name]) {
        this.settings.messages[element.name] = {};
    }
    previous.originalMessage = this.settings.messages[element.name].async;
    this.settings.messages[element.name].async = previous.message;

    if (previous.old === value) {
        return previous.valid;
    }

    previous.old = value;
    var validator = this;
    this.startRequest(element);
    var data = {};
    data[element.name] = value;
    var call = param.call
    var callback = param.callback !== undefined ? param.callback :
        function (response) { return response }
    call(value, function (response) {
        validator.settings.messages[element.name].async = previous.originalMessage;
        response = callback(response);
        var valid = response === true || response === "true";
        if (valid) {
            var submitted = validator.formSubmitted;
            validator.prepareElement(element);
            validator.formSubmitted = submitted;
            validator.successList.push(element);
            delete validator.invalid[element.name];
            validator.showErrors();
        } else {
            var errors = {};
            var message = response || validator.defaultMessage(element, "async");
            errors[element.name] = previous.message = $.isFunction(message) ? message(value) : message;
            validator.invalid[element.name] = true;
            validator.showErrors(errors);
        }
        previous.valid = valid;
        validator.stopRequest(element, valid);
    });
    return "pending";
}, jQuery.validator.messages.remote);

function validate(options) {
    var fields = options.fields

    // 生成 jQuery Validation 的 rules 和 messages
    var jv_rules = {}
    var jv_messages = {}
    for (var field in fields) { if (fields.hasOwnProperty(field)) {
        // rules 和 messages
        var rule = fields[field].rule
        var jv_rule = {}
        var jv_message = {}
        for (var i = 0; i < rule.length; i++) {
            if (!rule[i].disabled) {
                var argument = rule[i].argument
                jv_rule[rule[i].method] =
                    typeof argument !== 'undefined' ? argument : true
                var message = rule[i].message
                if (typeof message !== 'undefined') {
                    jv_message[rule[i].method] = message
                }
            }
        }
        jv_rules[field] = jv_rule
        jv_messages[field] = jv_message

        // 关联验证信息和HTML节点，在之后使用
        $("[name='" + field + "']").prop('validation_data', fields[field])
    }}

    $('form').validate({
        rules: jv_rules,
        messages: jv_messages,
        errorElement: 'span',
        errorPlacement: function (error, element) {
            var $e = $(element)
            var message = $(error).text()
            if ($e.prop('validation_message') !== message) { // 避免错误提示框重复显示引起的闪烁
                $e.prop('validation_message', message)
                $e.tooltip('destroy')
                $e.tooltip({
                    placement: $e.prop('validation_data').popover_placement,
                    trigger: 'manual',
                    title: message,
                    container: '.controls'
                });
                $e.tooltip('show')
            }
        },
        unhighlight: function(element, errorClass) {
            var $e = $(element);
            if ($e.prop('validation_message')) {
                $e.prop('validation_message', null)
                $e.tooltip('destroy')
                $e.removeClass(errorClass)
            }
        },
        submitHandler: function(form) {
            if (typeof options.onsubmit !== 'undefined') {
                options.onsubmit();
            }
            form.submit()
        },
        onkeyup: false
    })
}
