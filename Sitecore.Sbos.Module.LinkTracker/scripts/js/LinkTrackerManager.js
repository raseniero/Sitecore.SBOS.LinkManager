var triggerCount = 0; GoalCheck = "", PageEventCheck = "", popupCheck = 0;

function triggerGoal(goalId, shouldTriggerGoal, goalData) {
    $.ajax({
        url: "/Events/Handler/TrackedLinkHandler.ashx",
        type: "GET",
        data: { gid: goalId, triggerGoal: shouldTriggerGoal, goalData },
        context: this,
        success: function (data) {
            GoalCheck = shouldTriggerGoal;
            triggerCount++;
            TriggeredEventOnClick();
        },
        error: function (data) {
            alert("Goal is not been triggered", data);
        }
    });
}

function triggerPageEvent(pageEventId, shouldTriggerPageEvent, pageEventData) {
    $.ajax({
        url: "/Events/Handler/TrackedLinkHandler.ashx",
        type: "GET",
        data: { peid: pageEventId, triggerPageEvent: shouldTriggerPageEvent, pageEventData },
        context: this,
        success: function (data) {           
            PageEventCheck = shouldTriggerPageEvent;
            triggerCount++;
            TriggeredEventOnClick();
        },
        error: function (data) {
            alert("PageEvent is not been triggered", data);
        }
    });
}

function TriggeredEventOnClick() {
    var msg = "";
    
    if ((GoalCheck != "" && PageEventCheck != "") && popupCheck == 1) {
        popupCheck = 0;
    }

    if (triggerCount == 2 && popupCheck == 0) {
        if (GoalCheck == "true") {
            msg += "Goal ";
        }

        if (PageEventCheck == "true" && GoalCheck == "true") {
            msg += "and ";
        }

        if (PageEventCheck == "true") {
            msg += "PageEvent ";
        }

        if (PageEventCheck == "true" && GoalCheck == "true") {
            msg += "are ";
        } else {
            msg += "is ";
        }

        msg += "Triggered";

        if (PageEventCheck == "false" && GoalCheck == "false")
            popupCheck = 1;

        if(popupCheck == 0)
            alert(msg);

        popupCheck = 1;

        GoalCheck = "";
        PageEventCheck = "";
        triggerCount = 0;
    }
}