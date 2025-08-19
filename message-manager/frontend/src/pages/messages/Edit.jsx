import api from "../../api";
import React, { useEffect, useState, forwardRef } from "react";
import { useNavigate } from "react-router-dom";
import { useParams } from "react-router-dom";
import { Box, Button, Flex, Heading, HStack, VStack, Input, Field, Fieldset, Textarea, Separator, StackSeparator, Clipboard, Show } from "@chakra-ui/react";
import DateField from "../../components/DateField";
import DurationInput from "../../components/DurationInput";
import SeriesComboBox from "../../components/SeriesComboBox";
import { messageSchema } from "@message-manager/shared/schemas/index.js"


function formatTitle(title) {
  return title ?? "";
}

function formatDescription(description) {
  return description ?? "";
}

function getDateObject(dateString) {
  if (!dateString) return new Date();
  return new Date(dateString);
}

function _StackSeparator() {
  return <Box height={50} />;
}

function ClipboardButton({ value }) {
  return (
    <Clipboard.Root value={value}>
      <Clipboard.Trigger asChild>
        <Button variant="surface" size="sm" height="100%">
          <Clipboard.Indicator />
        </Button>
      </Clipboard.Trigger>
    </Clipboard.Root>
  );
}

const TextBox = forwardRef(({ value, ...props }, ref) => {
  return (
    <Textarea value={value}
      autoresize
      {...props}
      ref={ref} />
  );
});

const CopyableTextBox = forwardRef(({ value, ...props }, ref) => {
  return (
    <HStack align="stretch" w="100%">
      <TextBox value={value} {...props} ref={ref} />
      <ClipboardButton value={value} />
    </HStack>
  );
});

const AddButton = () => {
  return (
    <Button
      onClick={(e) => { }}
    >Add
    </Button>);
}

const SaveButton = ({ originalState, currentState }) => {
  const isDirty = JSON.stringify(originalState) !== JSON.stringify(currentState);
  return (<Button size="sm" disabled={!isDirty}>Save</Button>);
}

const EditBasicSettings = ({ message, setMessage }) => {
  return (
    <Fieldset.Root size="md">
      {/* Heading */}
      <Fieldset.Legend>
        <Heading size="lg" mb={0}>Basic Information</Heading>
      </Fieldset.Legend>

      <Separator mt={2} />

      {/* Fields */}
      <Field.Root mb={0} orientation="horizontal">
        <Field.Label>Title</Field.Label>
        <Input
          value={formatTitle(message?.title)}
          onChange={(e) => setMessage((prev) => ({ ...prev, title: e.target.value }))}
        />
      </Field.Root>
      <Field.Root mb={0} orientation="horizontal">
        <Field.Label>Description</Field.Label>
        <TextBox
          value={formatDescription(message?.description)}
          onChange={(e) => setMessage((prev) => ({ ...prev, description: e.target.value }))}
        />
      </Field.Root>
      <Field.Root mb={0} orientation="horizontal">
        <HStack justify="flex-start">
          <Field.Label>Date</Field.Label>
          <DateField
            value={getDateObject(message?.date)}
            setValue
            onChange={(date) => setMessage((prev) => ({ ...prev, date }))} />
        </HStack>
      </Field.Root>
      <Field.Root mb={0} orientation="horizontal">
        <Field.Label>Series</Field.Label>
        <SeriesComboBox
          value={message?.series}
          onValueChange={(e) => {
            const series = (e.value?.length == 1) ? e.value[0] : null;
            setMessage((prev) => ({ ...prev, series }));
          }}
        />
      </Field.Root>
    </Fieldset.Root>
  );
};


const EditNotes = ({ message, setMessage }) => {
  return (
    <Fieldset.Root size="md">
      {/* Heading */}
      <HStack justify="space-between" align="center">
        <Fieldset.Legend>
          <Heading size="lg" mb={0}>Notes</Heading>
        </Fieldset.Legend>
        {message?.notes === null && <AddButton />}
      </HStack>

      <Separator mt={2} />

      {/* Fields */}
      {message?.notes && <>
        <Field.Root mb={0} orientation="horizontal">
          <Field.Label>URL</Field.Label>
          <CopyableTextBox
            value={message.notes.url ?? ""}
            onChange={(e) => setMessage((prev) => ({ ...prev, notes: { ...prev.notes, url: e.target.value } }))} />
        </Field.Root>
      </>}
    </Fieldset.Root>);
}

const EditAudio = ({ message, setMessage }) => {
  return (
    <Fieldset.Root size="md">
      {/* Heading */}
      <HStack justify="space-between" align="center" verticalAlign="bottom">
        <Fieldset.Legend>
          <Heading size="lg">Audio</Heading>
        </Fieldset.Legend>
        {message?.audio === null && <AddButton />}
      </HStack>

      <Separator mt={2} />

      {/* Fields */}
      {message?.audio && <>
        <Field.Root mb={0} orientation="horizontal">
          <Field.Label>Download URL</Field.Label>
          <CopyableTextBox value={message.audio.downloadUrl ?? ""}
            onChange={(e) => setMessage((prev) => ({ ...prev, audio: { ...prev.audio, downloadUrl: e.value } }))} />
        </Field.Root>
        <Field.Root mb={0} orientation="horizontal">
          <Field.Label>Stream URL</Field.Label>
          <CopyableTextBox
            value={message.audio.streamUrl ?? ""}
            onChange={(e) => setMessage((prev) => ({ ...prev, audio: { ...prev.audio, streamUrl: e.target.value } }))} />
        </Field.Root>
      </>}
    </Fieldset.Root >
  );
};

const EditVideo = ({ message, setMessage }) => {
  return (
    <Fieldset.Root size="md">
      {/* Heading */}
      <HStack justify="space-between" align="center" mb={0}>
        <Fieldset.Legend>
          <Heading size="lg" mb={1}>Video</Heading>
        </Fieldset.Legend>
        {message?.video === null && <AddButton />}
      </HStack>

      <Separator mt={2} />

      {/* Fields */}
      {message?.video && <>
        <Field.Root mb={0} orientation="horizontal">
          <Field.Label>YouTube Video ID</Field.Label>
          <Input
            value={message.video.youTubeVideoId}
            onChange={(e) => setMessage((prev) => ({ ...prev, video: { ...prev.video, youTubeVideoId: e.target.value } }))} />
        </Field.Root>
        <Field.Root mb={0} orientation="horizontal" justifyContent="flex-start">
          <Field.Label>Start Time</Field.Label>
          <DurationInput
            value={message.video.messageStartTimeSeconds}
            onValueChange={(value) => setMessage((prev) => ({ ...prev, video: { ...prev.video, messageStartTimeSeconds: value } }))} />
        </Field.Root>
      </>}
    </Fieldset.Root>
  );
};

export function Edit() {
  const [originalMessage, setOriginalMessage] = useState(null);
  const [message, setMessage] = useState(null);
  const { id } = useParams();

  useEffect(() => {
    api.get(`/messages/${id}`).then((res) => {
      setMessage(res.data);
      setOriginalMessage(res.data);
    });
  }, [id]);

  console.log("message: ", message);

  return (
    <Box p={6} maxW="800px" mx="auto">
      <Heading size="2xl" mb={8}>Edit Message</Heading>
      <VStack gap={30} separator={<_StackSeparator />}>

        {/* Basic Information */}
        <EditBasicSettings message={message} setMessage={setMessage} />

        {/* Notes */}
        <EditNotes message={message} setMessage={setMessage} />

        {/* Audio */}
        <EditAudio message={message} setMessage={setMessage} />

        {/* Video */}
        <EditVideo message={message} setMessage={setMessage} />

        <Flex width="100%" justifyContent={"flex-end"}>
          <SaveButton originalState={originalMessage} currentState={message} />
        </Flex>
      </VStack>
    </Box>
  );
}
