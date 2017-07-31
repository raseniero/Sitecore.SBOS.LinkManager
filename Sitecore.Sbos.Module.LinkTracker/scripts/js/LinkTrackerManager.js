function triggerGoal(goalId, shouldTriggerGoal, goalData) {
    $.ajax({
        url: "/Events/Handler/TrackedLinkHandler.ashx",
        type: "GET",
        data: { gid: goalId, triggerGoal: shouldTriggerGoal, goalData },
        context: this
    });
}

function triggerPageEvent(pageEventId, shouldTriggerPageEvent, pageEventData) {
    $.ajax({
        url: "/Events/Handler/TrackedLinkHandler.ashx",
        type: "GET",
        data: { peid: pageEventId, triggerPageEvent: shouldTriggerPageEvent, pageEventData },
        context: this
    });
}